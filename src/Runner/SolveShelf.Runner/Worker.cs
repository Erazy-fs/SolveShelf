using Confluent.Kafka;

namespace SolveShelf.Runner;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Runner started. Waiting for messages...");
        logger.LogInformation("Runner started. Waiting for messages...");

        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "runner-group-1",     // важный момент: consumer group
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();

        consumer.Subscribe("runs");
        
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
            // потом вынесем это в конфиг/переменные окружения
        };

        var producer = new ProducerBuilder<string, string>(producerConfig).Build();

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                Console.WriteLine(
                    $"Received message from 'runs': Key={result.Message.Key}, Value={result.Message.Value}");
                logger.LogInformation("Received message from 'runs': Key={Key}, Value={Value}",
                    result.Message.Key, result.Message.Value);
                
                await Task.Delay(5000, stoppingToken);
                
                producer.ProduceAsync("results", new Message<string, string>
                {
                    Key = result.Message.Key,
                    Value = @"\u0027hello\u0027"
                });

            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}
