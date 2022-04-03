using System.Net.Mime;
using BookChangelog.API.Common;
using BookChangelog.API.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookChangelog.API.Features.Authors;

[ApiController]
[Route("authors")]
public class GetAuthorList : ControllerBase
{
    private readonly BookChangelogContext _context;

    public GetAuthorList(BookChangelogContext context) => _context = context;

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<PagedResponse<AuthorDto>> Action([FromQuery]GetAuthorListRequest request, CancellationToken cancellationToken)
    {
        var query = _context.Authors
            .Select(a => new AuthorDto(a.Id, a.Name));
        
        return PagedResponse<AuthorDto>.Create(query, request.PageNumber, request.PageSize, cancellationToken);
    }

    public record GetAuthorListRequest(int PageSize = 10, int PageNumber = 0)
    {
        public class Validator : AbstractValidator<GetAuthorListRequest>
        {
            public Validator()
            {
                RuleFor(r => r.PageSize).InclusiveBetween(1, 50);
                RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(0);
            }
        }
    }
}