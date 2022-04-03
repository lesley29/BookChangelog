using Microsoft.EntityFrameworkCore;

namespace BookChangelog.API.Common;

public record PagedResponse<T>(int PageSize, int CurrentPage, int TotalCount, IReadOnlyCollection<T> Items)
{
    public static async Task<PagedResponse<T>> Create(IQueryable<T> source, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);
        var items = await source
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResponse<T>(pageSize, pageNumber, totalCount, items);
    }
}