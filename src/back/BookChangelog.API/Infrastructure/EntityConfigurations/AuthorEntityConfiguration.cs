using BookChangelog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookChangelog.API.Infrastructure.EntityConfigurations;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("author");
        builder.HasIndex(a => a.Name).IsUnique();
    }
}