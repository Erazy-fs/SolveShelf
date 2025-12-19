using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SolveShelf.Contracts.Messages;
using SolveShelf.Infrastructure.Kafka;
using System.Text.Json;

namespace SolveShelf.Api.Kafka;

public class KafkaResultConsumer(IResultsStore results, IKafkaConsumerFactory consumerFactory, IOptions<KafkaOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = consumerFactory.Create(options.Value.ApiResultsGroupId);
        consumer.Subscribe(options.Value.ResultsTopic);

        Console.WriteLine("[API] Results consumer started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var cr = consumer.Consume(TimeSpan.FromMilliseconds(100));
                if (cr is null) { results.Cleanup(); continue; }

                var msg = JsonSerializer.Deserialize<SubmissionCompleted>(cr.Message.Value, KafkaJson.Options);
                Console.WriteLine($"[API] Result received for {cr.Message.Key}");
                if (msg is not null) results.Save(msg);

                results.Cleanup();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[API] Results consumer error: " + ex.Message);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}