namespace SolveShelf.Domain;

public class Problem
{
    public int Id { get; set; }
    public string ExternalId { get; set; } = null!;
    public SourceType Source { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Tests { get; set; } = null!;

    public ICollection<Solution> Solutions { get; set; } = [];
}
