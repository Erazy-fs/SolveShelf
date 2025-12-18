namespace SolveShelf.Contracts.Api;

public sealed class CreateSubmissionRequest
{
    public string Code { get; set; } = "";
    public string Tests { get; set; } = "";
}
