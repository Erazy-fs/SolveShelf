using SolveShelf.Contracts.Messages;

namespace SolveShelf.Api.Kafka;

public interface IResultsStore
{
    void Save(SubmissionCompleted result);
    SubmissionCompleted? Get(string runId);
    void Cleanup();
}