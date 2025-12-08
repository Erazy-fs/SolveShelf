using System.Text.Json;
using Confluent.Kafka;

namespace SolveShelf.Api.Kafka;

// Контракт для отправки задач на выполнение
public interface ISubmissionQueueProducer
{
    Task EnqueueAsync(string runId, string code, string tests, CancellationToken cancellationToken = default);
}

public class KafkaSubmissionProducer : ISubmissionQueueProducer
{
    private readonly IProducer<string, string> _producer;
    private const string TopicName = "runs"; // имя топика с задачами

    public KafkaSubmissionProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
            // потом вынесем это в конфиг/переменные окружения
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task EnqueueAsync(string runId, string code, string tests, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("EnqueueAsync");
        // Пример полезной нагрузки — можешь поменять потом структуру
        var payload = new
        {
            RunId = runId,
            Code = code,
            Tests = tests
        };

        var json = JsonSerializer.Serialize(payload);

        var message = new Message<string, string>
        {
            Key = runId,
            Value = json
        };

        // если Kafka не запущена — тут будет исключение, пока это норм
        await _producer.ProduceAsync(TopicName, message, cancellationToken);
        
        Console.WriteLine("EnqueueAsyncEnd");
    }
}