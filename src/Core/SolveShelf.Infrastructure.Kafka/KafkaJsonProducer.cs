using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace SolveShelf.Infrastructure.Kafka;

public sealed class KafkaJsonProducer : IKafkaProducer, IDisposable
{
    private readonly IProducer<string, string> _producer;

    public KafkaJsonProducer(IOptions<KafkaOptions> options)
    {
        var cfg = new ProducerConfig { BootstrapServers = options.Value.BootstrapServers };
        _producer = new ProducerBuilder<string, string>(cfg).Build();
    }

    public async Task ProduceAsync<T>(string topic, string key, T value, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(value, KafkaJson.Options);
        await _producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = json }, ct);
    }

    public void Dispose() => _producer.Dispose();
}
