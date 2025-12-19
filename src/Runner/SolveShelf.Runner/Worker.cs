using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SolveShelf.Contracts.Messages;
using SolveShelf.Infrastructure.Kafka;
using System.Text.Json;

namespace SolveShelf.Runner;

public class Worker(
    ILogger<Worker> logger,
    IKafkaConsumerFactory consumerFactory,
    IKafkaProducer producer,
    IOptions<KafkaOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = consumerFactory.Create(options.Value.RunnerGroupId);
        consumer.Subscribe(options.Value.RunsTopic);

        logger.LogInformation("Runner started. Waiting for messages...");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);
                logger.LogInformation("Received message from 'runs': Key={Key}, Value={Value}", result.Message.Key, result.Message.Value);

                var req = JsonSerializer.Deserialize<SubmissionRequested>(result.Message.Value, KafkaJson.Options);
                if (req is null)
                {
                    logger.LogWarning("Failed to deserialize SubmissionRequested. RunId={RunId}", req.RunId);
                    continue;
                }


                // имитация выполнения
                await Task.Delay(5000, stoppingToken);

                var completed = new SubmissionCompleted
                {
                    RunId = req.RunId,
                    Success = true,
                    Output = $"Fake result for {req.RunId}",
                    CompletedAtUtc = DateTime.UtcNow
                };

                await producer.ProduceAsync(options.Value.ResultsTopic, completed.RunId, completed, stoppingToken);
                logger.LogInformation("Sent result for {RunId}", completed.RunId);
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}
