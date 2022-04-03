using BookChangelog.API.Features.Authors;
using NodaTime;

namespace BookChangelog.API.Features.Books;

public record BookDto(Guid Id, string Title, string? Description, LocalDate PublicationDate,
    IReadOnlyCollection<AuthorDto> Authors);