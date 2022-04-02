namespace BookChangelog.API.Models;

public enum BookAuthorChangeType
{
    Added,
    Removed
}

public record BookAuthorChange(string Name, BookAuthorChangeType ChangeType);

public class BookChange
{
    public string? Title { get; init; }

    public string? Description { get; init; }

    public ICollection<BookAuthorChange>? AuthorsChanges { get; init; }
}

public class BookChangeHistory
{
    private BookChangeHistory(Guid bookId, int changeNumber, DateTime changeDateTime, BookChange change)
    {
        BookId = bookId;
        ChangeNumber = changeNumber;
        ChangeDateTime = changeDateTime;
        Change = change;
    }

    public Guid BookId { get; private set; }
    
    public int ChangeNumber { get; private set; }

    public DateTime ChangeDateTime { get; private set; }

    public BookChange Change { get; private set; }
}