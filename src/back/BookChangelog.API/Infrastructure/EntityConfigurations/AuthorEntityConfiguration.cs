using BookChangelog.API.Features.Authors;
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
        
        builder
            .HasMany(a => a.Books)
            .WithMany(b => b.Authors)
            .UsingEntity<BookAuthor>(
                b => b
                    .HasOne(ba => ba.Book)
                    .WithMany(a => a.BookAuthors)
                    .HasForeignKey(ba => ba.BookId),
                b => b
                    .HasOne(ba => ba.Author)
                    .WithMany(a => a.BookAuthors)
                    .HasForeignKey(ba => ba.AuthorId),
                b =>
                {
                    b.HasKey(ba => new { ba.AuthorId, ba.BookId });
                });
    }
}