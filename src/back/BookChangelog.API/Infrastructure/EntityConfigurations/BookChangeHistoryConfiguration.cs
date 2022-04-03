using System.Text.Json;
using System.Text.Json.Serialization;
using BookChangelog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookChangelog.API.Infrastructure.EntityConfigurations;

public class BookChangeHistoryConfiguration : IEntityTypeConfiguration<BookChangeHistory>
{
    public void Configure(EntityTypeBuilder<BookChangeHistory> builder)
    {
        builder.ToTable("book_change_history");

        builder.HasKey(bch => new { bch.BookId, bch.ChangeNumber });
        builder.Property(bch => bch.ChangeNumber).ValueGeneratedOnAdd();
        builder.Property(bch => bch.ChangeDateTime).HasDefaultValueSql("now()");

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() }
        };

        builder
            .Property(bch => bch.Change)
            .HasConversion(
                convertToProviderExpression: bookChange => JsonSerializer.Serialize(bookChange, serializerOptions),
                convertFromProviderExpression: str => JsonSerializer.Deserialize<BookChange>(str, serializerOptions)!);
    }
}