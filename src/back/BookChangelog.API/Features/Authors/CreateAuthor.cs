using Microsoft.AspNetCore.Mvc;

namespace BookChangelog.API.Features.Authors;

[ApiController]
[Route("authors")]
public class CreateAuthor : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorDto>> Action(CreateAuthorRequest request)
    {
        await Task.Delay(100);
        var authorId = Guid.NewGuid();

        return CreatedAtAction(
            actionName: nameof(GetAuthor.Action),
            controllerName: nameof(GetAuthor),
            routeValues: new { id = authorId },
            new AuthorDto(authorId, "pepka"));
    }
}

public record CreateAuthorRequest
{
    public string? Name { get; init; }
}