namespace SolveShelf.Contracts.Api;

public sealed class SubmissionStatusResponse
{
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    public bool? Success { get; set; }
    public string? Output { get; set; }
}
