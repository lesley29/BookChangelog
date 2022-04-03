using Microsoft.AspNetCore.Mvc;

namespace BookChangelog.API.Features.Books;

[ApiController]
[Route("books")]
public class UpdateBook : ControllerBase
{
    public UpdateBook()
    {
        
    }
    
    [HttpPut("{id}")]
    public async Task<BookDto> Action(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        return new BookDto();
    }

    public record UpdateBookRequest();
}