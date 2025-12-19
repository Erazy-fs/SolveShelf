using SolveShelf.Contracts.Messages;

namespace SolveShelf.Api.Kafka;

public interface ISubmissionQueueProducer
{
    Task EnqueueAsync(SubmissionRequested submissionRequested, CancellationToken cancellationToken = default);
}
