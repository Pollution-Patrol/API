namespace PollutionPatrol.Modules.Report.Infrastructure.Persistence;

internal sealed class ReportDbContext : DbContext, IReportDbContext
{
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    public ReportDbContext(DbContextOptions<ReportDbContext> options, IDomainEventsDispatcher domainEventsDispatcher)
        : base(options)
    {
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    public DbSet<Domain.ReportAggregate.Report> Reports { get; private set; }

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