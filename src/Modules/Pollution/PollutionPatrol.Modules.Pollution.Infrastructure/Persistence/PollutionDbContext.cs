namespace PollutionPatrol.Modules.Pollution.Infrastructure.Persistence;

internal sealed class PollutionDbContext : DbContext, IPollutionDbContext
{
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    public PollutionDbContext(DbContextOptions<PollutionDbContext> options, IDomainEventsDispatcher domainEventsDispatcher)
        : base(options)
    {
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    public DbSet<Report> Reports { get; private set; }

    public async Task CommitAsync()
    {
        await SaveChangesAsync();

        await _domainEventsDispatcher.DispatchEventsAsync(this);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        // use this assembly for executing dotnet commands. 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}