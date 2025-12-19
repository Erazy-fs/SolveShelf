using SolveShelf.Contracts.Messages;
using System.Collections.Concurrent;

namespace SolveShelf.Api.Kafka;

public class InMemoryResultsStore : IResultsStore
{
    private readonly ConcurrentDictionary<string, (SubmissionCompleted Result, DateTime Timestamp)> _store = new();
    private readonly TimeSpan _ttl = TimeSpan.FromMinutes(2);

    public void Save(SubmissionCompleted result) =>
        _store[result.RunId] = (result, DateTime.UtcNow);

    public SubmissionCompleted? Get(string runId) =>
        _store.TryGetValue(runId, out var entry) ? entry.Result : null;

    public void Cleanup()
    {
        var now = DateTime.UtcNow;
        foreach (var item in _store)
            if (now - item.Value.Timestamp > _ttl)
                _store.TryRemove(item.Key, out _);
    }
}