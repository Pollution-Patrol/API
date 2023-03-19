namespace PollutionPatrol.BuildingBlocks.Application.Collections;

public class ReadOnlyPagedList<TSource> : BasePagedList<TSource>
{
    private ReadOnlyPagedList(IList<TSource> items, int count, int page, int size) 
        : base(items, count, page, size)
    {
    }

    public static async Task<ReadOnlyPagedList<TSource>> ToReadOnlyPagedListAsync(IQueryable<TSource> source, int page, int size)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * size).Take(size).ToListAsync();

        return new ReadOnlyPagedList<TSource>(items, count, page, size);
    }
}