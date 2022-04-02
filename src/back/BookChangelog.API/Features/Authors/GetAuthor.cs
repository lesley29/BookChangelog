using Microsoft.AspNetCore.Mvc;

namespace BookChangelog.API.Features.Authors;

[ApiController]
[Route("authors")]
public class GetAuthor : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<AuthorDto> Action(Guid id)
    {
        return new AuthorDto(id, "pepka");
    }
}