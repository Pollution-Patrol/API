namespace PollutionPatrol.Modules.UserAccess.Infrastructure.Persistence;

internal sealed class UserAccessDbContext : DbContext, IUserAccessDbContext
{
    private readonly IDomainEventsDispatcher _eventsDispatcher;

    public UserAccessDbContext(DbContextOptions<UserAccessDbContext> options, IDomainEventsDispatcher eventsDispatcher)
        : base(options)
    {
        _eventsDispatcher = eventsDispatcher;
    }

    public DbSet<Registration> Registrations { get; private set; }
    public DbSet<ApplicationUser> Users { get; private set; }

    public async Task CommitAsync()
    {
        await SaveChangesAsync();

        await _eventsDispatcher.DispatchEventsAsync(this);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // use this assembly for executing dotnet commands. 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}