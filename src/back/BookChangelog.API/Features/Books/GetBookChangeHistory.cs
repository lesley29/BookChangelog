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
            .AsNoTracking()
            .Select(b => new
            {
                b.Id, 
                b.ChangeHistory
            })
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (book is null)
        {
            return NotFound();
        }

        var changeHistory = new List<BookChangeHistoryDto>(book.ChangeHistory
            .Where(ch => ch.Change.AuthorsChanges == null)
            .Select(ch =>
                new BookChangeHistoryDto(ch.ChangeNumber, ch.ChangeDateTime, 
                    new BookChangeDto
                    {
                        Title = ch.Change.Title,
                        Description = ch.Change.Description, 
                        PublicationDate = ch.Change.PublicationDate
                    })));

        var authorsChanges = book.ChangeHistory
            .Where(ch => ch.Change.AuthorsChanges != null)
            .ToList(); 
        
        var authorsToEnrichWith = authorsChanges
            .SelectMany(ch => ch.Change.AuthorsChanges!.Select(ac => ac.Id))
            .Distinct()
            .ToList();

        if (authorsToEnrichWith.Any())
        {
            var authors = await _context.Authors
                .AsNoTracking()
                .Where(a => authorsToEnrichWith.Contains(a.Id))
                .ToDictionaryAsync(a => a.Id, cancellationToken);
            
            changeHistory.AddRange(authorsChanges.Select(ac => 
                new BookChangeHistoryDto(ac.ChangeNumber, ac.ChangeDateTime,
                    new BookChangeDto
                    {
                        Title = ac.Change.Title, 
                        Description = ac.Change.Description,
                        PublicationDate = ac.Change.PublicationDate, 
                        AuthorsChanges = ac.Change.AuthorsChanges!.Select(ch => 
                            new BookAuthorChangeDto(ch.Id, authors[ch.Id].Name, ch.ChangeType)).ToList()
                    })));
        }

        return Ok(changeHistory.OrderByDescending(ch => ch.ChangeNumber));
    }

    public record BookAuthorChangeDto(Guid Id, string Name, BookAuthorChangeType ChangeType);

    public record BookChangeDto
    {
        public string? Title { get; init; }
        
        public string? Description { get; init; }
        
        public LocalDate? PublicationDate { get; init; }
        
        public IReadOnlyCollection<BookAuthorChangeDto>? AuthorsChanges { get; init; }
    }

    public record BookChangeHistoryDto(int ChangeNumber, Instant ChangeDateTime, BookChangeDto Change);
}