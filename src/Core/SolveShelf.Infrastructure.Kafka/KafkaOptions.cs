namespace SolveShelf.Infrastructure.Kafka;

public sealed class KafkaOptions
{
    public string BootstrapServers { get; set; } = "localhost:9092";
    public string RunsTopic { get; set; } = "runs";
    public string ResultsTopic { get; set; } = "results";
    public string RunnerGroupId { get; set; } = "runner-group-1";
    public string ApiResultsGroupId { get; set; } = "results-main";
}
