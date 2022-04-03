using NodaTime;

namespace BookChangelog.API.Models;

public class Book
{
    private readonly List<Author> _authors;
    private readonly List<BookAuthor> _bookAuthors;
    private readonly List<BookChangeHistory> _changeHistory;

    public Book(Guid id, string title, string? description, LocalDate publicationDate)
    {
        Id = id;
        Title = title;
        Description = description;
        PublicationDate = publicationDate;

        _authors = new List<Author>();
        _bookAuthors = new List<BookAuthor>();
        _changeHistory = new List<BookChangeHistory>();
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public LocalDate PublicationDate { get; private set; }

    public IReadOnlyCollection<Author> Authors => _authors;

    public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors;

    public IReadOnlyCollection<BookChangeHistory> ChangeHistory => _changeHistory;

    public void AddAuthors(IReadOnlyCollection<Guid> authorIds)
    {
        _bookAuthors.AddRange(authorIds.Select(authorId => new BookAuthor(authorId, Id)));
    }

    public void Update(string title, string? description, LocalDate publicationDate, IReadOnlyCollection<Guid> authors)
    {
        var changed = false;
        var change = new BookChange();
        
        if (Title != title)
        {
            Title = title;
            change.Title = title;
            changed = true;
        }

        if (Description != description)
        {
            Description = description;
            change.Description = description;
            changed = true;
        }

        if (PublicationDate != publicationDate)
        {
            PublicationDate = publicationDate;
            change.PublicationDate = publicationDate;
            changed = true;
        }

        var existingBookAuthorsIds = _bookAuthors.Select(ba => ba.AuthorId).ToList();

        if (!existingBookAuthorsIds.SequenceEqual(authors))
        {
            var newAuthors = authors
                .Except(existingBookAuthorsIds)
                .Select(aId => new BookAuthorChange(aId, BookAuthorChangeType.Added));
            
            var removedAuthors = existingBookAuthorsIds
                .Except(authors)
                .Select(aId => new BookAuthorChange(aId, BookAuthorChangeType.Removed));
            
            _bookAuthors.Clear();
            _bookAuthors.AddRange(authors.Select(a => new BookAuthor(a, Id)));

            change.AuthorsChanges = newAuthors.Union(removedAuthors).ToList();
            changed = true;
        }

        if (changed)
        {
            _changeHistory.Add(new BookChangeHistory(Id, change));
        }
    }
}