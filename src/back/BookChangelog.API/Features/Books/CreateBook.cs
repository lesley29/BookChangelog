using Microsoft.AspNetCore.Mvc;

namespace BookChangelog.API.Features.Books;

[ApiController]
[Route("books")]
public class CreateBookController : ControllerBase
{
    public CreateBookController()
    {
        
    }
    
    [HttpPost]
    public async Task<BookDto> Action(CreateBookRequest request, CancellationToken cancellationToken)
    {
        return new BookDto();
    }

    public record CreateBookRequest();
}