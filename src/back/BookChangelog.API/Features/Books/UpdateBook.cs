using Microsoft.AspNetCore.Mvc;
using NodaTime;

namespace BookChangelog.API.Features.Books;

[ApiController]
[Route("books")]
public class UpdateBook : ControllerBase
{
    public UpdateBook()
    {
        
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<BookDto>> Action(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        return new BookDto(Guid.NewGuid(), "title", "description", LocalDate.MinIsoValue, new[] { Guid.NewGuid() });
    }

    public record UpdateBookRequest;
}