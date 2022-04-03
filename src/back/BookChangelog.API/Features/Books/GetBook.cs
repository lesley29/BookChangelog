using Microsoft.AspNetCore.Mvc;

namespace BookChangelog.API.Features.Books;

[ApiController]
[Route("books")]
public class GetBook : ControllerBase
{
    public GetBook()
    {
        
    }

    [HttpGet("{id}")]
    public async Task<BookDto> Action(Guid id, CancellationToken cancellationToken)
    {
        return new BookDto();
    }
}