namespace BookChangelog.API.Models;

public class Book
{
    private readonly List<Author> _authors;
    
    public Book(Guid id, string title, string? description, DateOnly publicationDate)
    {
        Id = id;
        Title = title;
        Description = description;
        PublicationDate = publicationDate;

        _authors = new List<Author>();
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public DateOnly PublicationDate { get; private set; }

    public IReadOnlyCollection<Author> Authors => _authors;
}