using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace SolveShelf.Infrastructure.Kafka;

public sealed class KafkaConsumerFactory(IOptions<KafkaOptions> options) : IKafkaConsumerFactory
{
    public IConsumer<string, string> Create(string groupId)
    {
        var cfg = new ConsumerConfig
        {
            BootstrapServers = options.Value.BootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        return new ConsumerBuilder<string, string>(cfg).Build();
    }
}
