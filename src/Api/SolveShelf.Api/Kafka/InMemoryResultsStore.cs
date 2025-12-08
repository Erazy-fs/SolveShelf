using System.Collections.Concurrent;

namespace SolveShelf.Api.Kafka;

public class InMemoryResultsStore : IResultsStore
{
    private readonly ConcurrentDictionary<string, (string Result, DateTime Timestamp)> _store = new();
    private readonly TimeSpan _ttl = TimeSpan.FromMinutes(2);

    public void Save(string runId, string json)
    {
        _store[runId] = (json, DateTime.UtcNow);
    }

    public string? Get(string runId)
    {
        if (_store.TryGetValue(runId, out var entry))
            return entry.Result;

        return null;
    }

    public void Cleanup()
    {
        var now = DateTime.UtcNow;

        foreach (var item in _store)
        {
            if (now - item.Value.Timestamp > _ttl)
            {
                _store.TryRemove(item.Key, out _);
            }
        }
    }
}