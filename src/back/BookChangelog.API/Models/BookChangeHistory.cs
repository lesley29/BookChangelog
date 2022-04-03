using NodaTime;

namespace BookChangelog.API.Models;

public enum BookAuthorChangeType
{
    Added,
    Removed
}

public record BookAuthorChange(Guid Id, BookAuthorChangeType ChangeType);

public record BookChange
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public LocalDate? PublicationDate { get; set; }

    public ICollection<BookAuthorChange>? AuthorsChanges { get; set; }
}

public class BookChangeHistory
{
    private BookChangeHistory(Guid bookId, int changeNumber, Instant changeDateTime, BookChange change)
    {
        BookId = bookId;
        ChangeNumber = changeNumber;
        ChangeDateTime = changeDateTime;
        Change = change;
    }

    public BookChangeHistory(Guid bookId, BookChange change)
    {
        BookId = bookId;
        Change = change;
    }

    public Guid BookId { get; private set; }
    
    public int ChangeNumber { get; private set; }

    public Instant ChangeDateTime { get; private set; }

    public BookChange Change { get; private set; }

    public Book Book { get; private set; } = null!;
}