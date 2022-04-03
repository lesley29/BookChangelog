using System.Net.Mime;
using BookChangelog.API.Infrastructure;
using BookChangelog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace BookChangelog.API.Features.Books;

[ApiController]
[Route("api/books")]
public class GetBookChangeHistory : ControllerBase
{
    private readonly BookChangelogContext _context;

    public GetBookChangeHistory(BookChangelogContext context) => _context = context;


    [HttpGet("{id}/change-history")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BookChangeHistoryDto>>> Action(Guid id, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Include(b => b.ChangeHistory)
            .Select(b => new
            {
                b.Id,
                ChangeHistory = b.ChangeHistory.Select(ch
                    => new BookChangeHistoryDto(ch.ChangeNumber, ch.ChangeDateTime, BookChangeDto.FromDbModel(ch.Change)))
            })
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book.ChangeHistory);
    }

    public record BookAuthorChangeDto(Guid Id, string Name, BookAuthorChangeType ChangeType);

    public record BookChangeDto(string? Title, string? Description,
        LocalDate? PublicationDate, IReadOnlyCollection<BookAuthorChangeDto>? AuthorsChanges)
    {
        public static BookChangeDto FromDbModel(BookChange change)
            => new(change.Title, change.Description, change.PublicationDate,
                change.AuthorsChanges?.Select(ac => new BookAuthorChangeDto(ac.Id, "TODO", ac.ChangeType)).ToList());
    };

    public record BookChangeHistoryDto(int ChangeNumber, Instant ChangeDateTime, BookChangeDto Change);
}