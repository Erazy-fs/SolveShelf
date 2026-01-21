using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolveShelf.Domain;

namespace SolveShelf.Infrastructure.Persistence.Configurations;

public sealed class SolutionConfiguration : IEntityTypeConfiguration<Solution>
{
    private const int ExternalIdMaxLen = 128;

    public void Configure(EntityTypeBuilder<Solution> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalId)
            .HasMaxLength(ExternalIdMaxLen)
            .IsRequired();

        builder.Property(x => x.Code)
            .IsRequired();

        builder.HasIndex(x => new { x.ProblemId, x.ExternalId })
            .IsUnique();

        builder.HasOne(x => x.Problem)
            .WithMany(x => x.Solutions)
            .HasForeignKey(x => x.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Language)
            .WithMany()
            .HasForeignKey(x => x.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
