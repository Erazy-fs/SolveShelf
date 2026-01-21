using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolveShelf.Domain;

namespace SolveShelf.Infrastructure.Persistence.Configurations;

public sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    private const int CodeMaxLen = 64;
    private const int NameMaxLen = 128;

    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .HasMaxLength(CodeMaxLen)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(NameMaxLen)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}
