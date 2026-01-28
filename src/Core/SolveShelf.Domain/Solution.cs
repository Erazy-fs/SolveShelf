namespace SolveShelf.Domain;

public class Solution
{
    public int Id { get; set; }
    public string ExternalId { get; set; } = null!;

    public int ProblemId { get; set; }
    public Problem Problem { get; set; } = null!;

    public int LanguageId { get; set; }
    public Language Language { get; set; } = null!;

    public string Code { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}
