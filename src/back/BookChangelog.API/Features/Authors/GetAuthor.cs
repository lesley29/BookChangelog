using System.Net.Mime;
using BookChangelog.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookChangelog.API.Features.Authors;

[ApiController]
[Route("authors")]
public class GetAuthor : ControllerBase
{
    private readonly BookChangelogContext _context;

    public GetAuthor(BookChangelogContext context) => _context = context;

    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorDto>> Action(Guid id, CancellationToken cancellationToken)
    {
        var author = await _context.Authors
            .Where(a => a.Id == id)
            .Select(a => new AuthorDto(a.Id, a.Name))
            .FirstOrDefaultAsync(cancellationToken);

        if (author is null)
        {
            return NotFound();
        }
        
        return Ok(author);
    }
}