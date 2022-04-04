namespace BookChangelog.API.Models;

public class Author
{
    private readonly List<Book> _books;
    private readonly List<BookAuthor> _bookAuthors;
    
    public Author(Guid id, string name)
    {
        Id = id;
        Name = name;

        _books = new List<Book>();
        _bookAuthors = new List<BookAuthor>();
    }

    public Guid Id { get; private set; }
    
    public string Name { get; private set; }

    public IReadOnlyCollection<Book> Books => _books;

    public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors;
}