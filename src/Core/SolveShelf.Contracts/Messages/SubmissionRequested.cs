namespace SolveShelf.Contracts.Messages;

public sealed class SubmissionRequested
{
    public string RunId { get; set; } = "";
    public string Code { get; set; } = "";
    public string Tests { get; set; } = "";
}
