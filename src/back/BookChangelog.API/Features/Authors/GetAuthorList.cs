using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using BookChangelog.API.Common;
using BookChangelog.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookChangelog.API.Features.Authors;

[ApiController]
[Route("authors")]
public class GetAuthorList : ControllerBase
{
    private readonly BookChangelogContext _context;

    public GetAuthorList(BookChangelogContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<PagedResponse<AuthorDto>> Action([FromQuery]GetAuthorListRequest request, CancellationToken cancellationToken)
    {
        var query = _context.Authors
            .AsNoTracking()
            .Select(a => new AuthorDto(a.Id, a.Name));
        
        return PagedResponse<AuthorDto>.Create(query, request.PageNumber, request.PageSize, cancellationToken);
    }

    public record GetAuthorListRequest
    {
        [Range(1, 50)]
        public int PageSize { get; init; } = 10;

        [Range(0, int.MaxValue)]
        public int PageNumber { get; init; } = 0;
    }
}