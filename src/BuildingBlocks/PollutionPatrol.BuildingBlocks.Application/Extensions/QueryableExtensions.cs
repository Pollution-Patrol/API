namespace PollutionPatrol.BuildingBlocks.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedList<TSource>> ToPagedListAsync<TSource>(this IQueryable<TSource> source, int page, int size) =>
        await PagedList<TSource>.ToPagedListAsync(source, page, size);
    
    public static async Task<ReadOnlyPagedList<TSource>> ToReadOnlyPagedListAsync<TSource>(this IQueryable<TSource> source, int page, int size) =>
        await ReadOnlyPagedList<TSource>.ToReadOnlyPagedListAsync(source, page, size);
}