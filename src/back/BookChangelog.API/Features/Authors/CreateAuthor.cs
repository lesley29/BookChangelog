using System.Net.Mime;
using BookChangelog.API.Infrastructure;
using BookChangelog.API.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookChangelog.API.Features.Authors;

[ApiController]
[Route("authors")]
public class CreateAuthor : ControllerBase
{
    private readonly BookChangelogContext _context;

    public CreateAuthor(BookChangelogContext context) => _context = context;

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<AuthorDto>> Action(CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        var author = new Author(Guid.NewGuid(), request.Name.Trim());

        if (await _context.Authors.AnyAsync(a => a.Name == author.Name, cancellationToken))
        {
            return Problem(
                detail: "Such author already exists",
                statusCode: StatusCodes.Status422UnprocessableEntity);
        }
        
        _context.Authors.Add(author);
        await _context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(
            actionName: nameof(GetAuthor.Action),
            controllerName: nameof(GetAuthor),
            routeValues: new { id = author.Id },
            new AuthorDto(author.Id, author.Name));
    }
}

public record CreateAuthorRequest(string Name)
{
    public class Validator : AbstractValidator<CreateAuthorRequest>
    {
        public Validator() => RuleFor(r => r.Name).NotEmpty();
    }
}