using System.Net.Mime;
using BookChangelog.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookChangelog.API.Features.Books;

[ApiController]
[Route("books")]
public class GetBook : ControllerBase
{
    private readonly BookChangelogContext _context;

    public GetBook(BookChangelogContext context) => _context = context;

    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDto>> Action(Guid id, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Select(b => new BookDto(b.Id, b.Title, b.Description, b.PublicationDate, 
                b.BookAuthors.Select(ba => ba.AuthorId).ToList()))
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (book is null)
        {
            return NotFound();
        }
        
        return Ok(book);
    }
}