namespace PollutionPatrol.BuildingBlocks.Application.Collections;

public sealed class PagedList<TSource> : BasePagedList<TSource>
{
    private PagedList(IEnumerable<TSource> items, int count, int page, int size)
        : base(items, count, page, size)
    {
    }

    public static async Task<PagedList<TSource>> ToPagedListAsync(IQueryable<TSource> source, int page, int size)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * size).Take(size).ToListAsync();

        return new PagedList<TSource>(items, count, page, size);
    }
}