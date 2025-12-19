using Confluent.Kafka;

namespace SolveShelf.Infrastructure.Kafka;

public interface IKafkaConsumerFactory
{
    IConsumer<string, string> Create(string groupId);
}
