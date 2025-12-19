using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SolveShelf.Contracts.Messages;
using SolveShelf.Infrastructure.Kafka;

namespace SolveShelf.Api.Kafka;

public class SubmissionQueueProducer(IKafkaProducer producer, IOptions<KafkaOptions> options) : ISubmissionQueueProducer
{
    public async Task EnqueueAsync(SubmissionRequested submissionRequested, CancellationToken cancellationToken = default)
    {
        await producer.ProduceAsync(options.Value.RunsTopic, submissionRequested.RunId, submissionRequested, cancellationToken);
    }
}