using Confluent.Kafka;

namespace SolveShelf.Api.Kafka;

public class KafkaResultConsumer : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly IResultsStore _results;

    public KafkaResultConsumer(IResultsStore results)
    {
        _results = results;

        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "results-main",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _consumer.Subscribe("results");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("[API] Results consumer started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var cr = _consumer.Consume(TimeSpan.FromMilliseconds(100));
                if (cr != null)
                {
                    Console.WriteLine($"[API] Result received for {cr.Message.Key}");
                    _results.Save(cr.Message.Key, cr.Message.Value);
                }

                _results.Cleanup();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[API] Consumer error: " + ex.Message);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}