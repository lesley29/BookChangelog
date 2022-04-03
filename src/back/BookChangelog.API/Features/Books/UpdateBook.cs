using System.Net.Mime;
using BookChangelog.API.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace BookChangelog.API.Features.Books;

[ApiController]
[Route("api/books")]
public class UpdateBook : ControllerBase
{
    private readonly BookChangelogContext _context;

    public UpdateBook(BookChangelogContext context) => _context = context;

    [HttpPut("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Action(Guid id, UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Include(b => b.BookAuthors)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (book is null)
        {
            return NotFound();
        }
        
        var authors = await _context.Authors
            .Where(a => request.Authors.Contains(a.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (authors.Count != request.Authors.Count)
        {
            return Problem(
                detail: "Some of the authors don't exist",
                statusCode: StatusCodes.Status422UnprocessableEntity);
        }
        
        book.Update(request.Title, request.Description, request.PublicationDate, request.Authors);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return NoContent();
    }

    public record UpdateBookRequest(string Title, string? Description, LocalDate PublicationDate,
        IReadOnlyCollection<Guid> Authors)
    {
        public class Validator : AbstractValidator<UpdateBookRequest>
        {
            public Validator()
            {
                RuleFor(r => r.Title).NotEmpty();
                RuleFor(r => r.Description).NotEmpty().When(r => r.Description is not null);
                RuleFor(r => r.PublicationDate).NotEqual(LocalDate.MinIsoValue);
                RuleFor(r => r.Authors).NotEmpty();
                RuleForEach(r => r.Authors).NotEmpty();
            }
        }
    };
}