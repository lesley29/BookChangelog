using Microsoft.EntityFrameworkCore;

namespace BookChangelog.API.Common;

public class PagedResponse<T>
{
    public PagedResponse(IReadOnlyCollection<T> items, int pageSize, int currentPage, int totalCount)
    {
        Items = items;
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalCount = totalCount;
    }

    public int TotalCount { get; }

    public int CurrentPage { get; }

    public int PageSize { get; }

    public IReadOnlyCollection<T> Items { get; }

    public static async Task<PagedResponse<T>> Create(IQueryable<T> source, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);
        var items = await source
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResponse<T>(items, pageSize, pageNumber, totalCount);
    }
}