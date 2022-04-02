namespace BookChangelog.API.Models;

public class Author
{
    private readonly List<Book> _books;
    
    public Author(Guid id, string name)
    {
        Id = id;
        Name = name;

        _books = new List<Book>();
    }

    public Guid Id { get; private set; }
    
    public string Name { get; private set; }

    public IReadOnlyCollection<Book> Books => _books;
}