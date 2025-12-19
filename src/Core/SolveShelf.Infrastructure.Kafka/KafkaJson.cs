using System.Text.Json;

namespace SolveShelf.Infrastructure.Kafka;

public sealed class KafkaJson
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };
}
