namespace SolveShelf.Contracts.Messages;

public sealed class SubmissionCompleted
{
    public string RunId { get; set; } = "";
    public bool Success { get; set; }
    public string Output { get; set; } = "";
    public DateTime CompletedAtUtc { get; set; }
}
