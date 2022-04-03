using BookChangelog.API.Models;

namespace BookChangelog.API.Features.Authors;

public record AuthorDto(Guid Id, string Name)
{
    public static AuthorDto FromDbModel(Author author) => new(author.Id, author.Name);
};