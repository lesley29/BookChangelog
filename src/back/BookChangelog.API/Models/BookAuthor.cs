namespace BookChangelog.API.Models;

public class BookAuthor
{
    public BookAuthor(Guid authorId, Guid bookId)
    {
        AuthorId = authorId;
        BookId = bookId;
    }

    public Guid AuthorId { get; private set; }

    public Guid BookId { get; private set; }

    public Author Author { get; private set; } = null!;
    
    public Book Book { get; private set; } = null!;
}