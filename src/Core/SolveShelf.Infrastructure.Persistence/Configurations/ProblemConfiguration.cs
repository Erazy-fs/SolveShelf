using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolveShelf.Domain;

namespace SolveShelf.Infrastructure.Persistence.Configurations;

public sealed class ProblemConfiguration : IEntityTypeConfiguration<Problem>
{
    private const int ExternalIdMaxLen = 128;
    private const int NameMaxLen = 256;

    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalId)
            .HasMaxLength(ExternalIdMaxLen)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(NameMaxLen)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.Tests)
            .IsRequired();

        builder.HasIndex(x => new { x.Source, x.ExternalId })
            .IsUnique();
    }
}
