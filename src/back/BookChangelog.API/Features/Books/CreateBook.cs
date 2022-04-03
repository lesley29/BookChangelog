using System.Net.Mime;
using BookChangelog.API.Infrastructure;
using BookChangelog.API.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace BookChangelog.API.Features.Books;

[ApiController]
[Route("books")]
public class CreateBookController : ControllerBase
{
    private readonly BookChangelogContext _context;

    public CreateBookController(BookChangelogContext context) => _context = context;

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<BookDto>> Action(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var authorCount = await _context.Authors
            .Where(a => request.Authors.Contains(a.Id))
            .CountAsync(cancellationToken);

        if (authorCount != request.Authors.Count)
        {
            return Problem(
                detail: "Some of the authors don't exist",
                statusCode: StatusCodes.Status422UnprocessableEntity);
        }
        
        var book = new Book(Guid.NewGuid(), request.Title, request.Description, request.PublicationDate);
        book.AddAuthors(request.Authors);
        _context.Books.Add(book);
        await _context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(
            actionName: nameof(GetBook.Action),
            controllerName: nameof(GetBook),
            routeValues: new { id = book.Id },
            new BookDto(book.Id, book.Title, book.Description, request.PublicationDate, request.Authors));
    }

    public record CreateBookRequest(string Title, string? Description, LocalDate PublicationDate,
        IReadOnlyCollection<Guid> Authors)
    {
        public class Validator : AbstractValidator<CreateBookRequest>
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
    }
}