using System.Net.Mime;
using BookChangelog.API.Common;
using BookChangelog.API.Infrastructure;
using BookChangelog.API.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace BookChangelog.API.Features.Books;

[ApiController]
[Route("books")]
public class GetBookList : ControllerBase
{
    private readonly BookChangelogContext _context;

    public GetBookList(BookChangelogContext context) => _context = context;

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResponse<BookDto>>> Action([FromQuery]GetBookListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _context.Books
            .Include(b => b.BookAuthors)
            .AsQueryable();

        query = ApplyPublicationDateFilter(query, request.PublishedFrom, request.PublishedTo);
        query = ApplySorting(query, request.SortBy, request.SortDirection);

        var projectedQuery = query
            .Select(b => new BookDto(b.Id, b.Title, b.Description, b.PublicationDate, 
                b.BookAuthors.Select(ba => ba.AuthorId).ToList()));
        
        return await PagedResponse<BookDto>.Create(projectedQuery, request.PageNumber, request.PageSize, cancellationToken);
    }
    
    private static IQueryable<Book> ApplyPublicationDateFilter(IQueryable<Book> currentQuery,
        LocalDate? publishedFrom, LocalDate? publishedTo)
    {
        if (publishedFrom is not null)
        {
            currentQuery = currentQuery.Where(b => b.PublicationDate >= publishedFrom);
        }

        if (publishedTo is not null)
        {
            currentQuery = currentQuery.Where(b => b.PublicationDate < publishedTo);
        }

        return currentQuery;
    }

    private static IQueryable<Book> ApplySorting(IQueryable<Book> currentQuery, 
        string? sortBy, string? sortDirection)
    {
        if (sortBy is null)
        {
            return currentQuery.OrderBy(r => r.Id);
        }
        
        var direction = sortDirection ?? SortDirections.Ascending;
        IOrderedQueryable<Book> orderedQuery;
        
        if (sortBy == SortableFields.Title)
        {
            orderedQuery = direction == SortDirections.Ascending
                ? currentQuery.OrderBy(d => d.Title)
                : currentQuery.OrderByDescending(d => d.Title);
        }
        else
        {
            orderedQuery = direction == SortDirections.Ascending
                ? currentQuery.OrderBy(d => d.PublicationDate)
                : currentQuery.OrderByDescending(d => d.PublicationDate);
        }

        return orderedQuery.ThenBy(r => r.Id);
    }

    private static class SortableFields
    {
        public const string PublicationDate = nameof(Book.PublicationDate);
        public const string Title = nameof(Book.Title);
    }

    private static class SortDirections
    {
        public const string Ascending = "asc";
        public const string Descending = "desc";
    }
    
    public record GetBookListRequest
    {
        public LocalDate? PublishedFrom { get; init; }
        
        public LocalDate? PublishedTo { get; init; }

        public string? SortBy { get; init; }
        
        public string? SortDirection { get; init; }

        public int PageSize { get; init; } = 50;

        public int PageNumber { get; init; } = 0;
        
        public class Validator : AbstractValidator<GetBookListRequest>
        {
            private static readonly string[] ValidSortByValues =
                { SortableFields.Title, SortableFields.PublicationDate };

            private static readonly string[] ValidSortDirections =
                { SortDirections.Ascending, SortDirections.Descending };
            
            public Validator()
            {
                RuleFor(r => r.PublishedFrom).NotEqual(LocalDate.MinIsoValue).When(r => r.PublishedFrom is not null);
                RuleFor(r => r.PublishedTo).NotEqual(LocalDate.MinIsoValue).When(r => r.PublishedTo is not null);
                When(r => r.PublishedFrom is not null && r.PublishedTo is not null, () =>
                {
                    RuleFor(r => r.PublishedFrom)
                        .Must((r, from) => from.Value < r.PublishedFrom.Value)
                        .WithMessage($"{nameof(PublishedFrom)} should be less than ${nameof(PublishedTo)}");
                });

                RuleFor(r => r.SortBy)
                    .Must(v => ValidSortByValues.Contains(v))
                    .When(r => r.SortBy is not null)
                    .WithMessage($"{nameof(SortBy)} should have one of the following values: " +
                                 string.Join(',', ValidSortByValues));

                RuleFor(r => r.SortDirection)
                    .Must(v => ValidSortByValues.Contains(v))
                    .When(r => r.SortDirection is not null)
                    .WithMessage($"{nameof(SortDirection)} should have one of the following values: " + 
                                 string.Join(',', ValidSortDirections));
                
                RuleFor(r => r.PageSize).InclusiveBetween(1, 50);
                RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(0);
            }
        }
    }
}