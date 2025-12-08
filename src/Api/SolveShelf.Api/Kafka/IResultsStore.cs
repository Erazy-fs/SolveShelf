using System.Collections.Concurrent;

namespace SolveShelf.Api.Kafka;

public interface IResultsStore
{
    void Save(string runId, string json);
    string? Get(string runId);
    void Cleanup();
}