using BookChangelog.API.Features.Authors;
using NodaTime;

namespace BookChangelog.API.Models;

public class Book
{
    private readonly List<Author> _authors;
    private readonly List<BookAuthor> _bookAuthors;

    public Book(Guid id, string title, string? description, LocalDate publicationDate)
    {
        Id = id;
        Title = title;
        Description = description;
        PublicationDate = publicationDate;

        _authors = new List<Author>();
        _bookAuthors = new List<BookAuthor>();
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public LocalDate PublicationDate { get; private set; }

    public IReadOnlyCollection<Author> Authors => _authors;

    public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors;

    public void AddAuthors(IReadOnlyCollection<Guid> authorIds)
    {
        _bookAuthors.AddRange(authorIds.Select(authorId => new BookAuthor(authorId, Id)));
    }
}