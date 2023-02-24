namespace PollutionPatrol.Modules.UserAccess.Infrastructure.Persistence;

internal sealed class UserAccessDbContextFactory : IDesignTimeDbContextFactory<UserAccessDbContext>
{
    public UserAccessDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connection = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<UserAccessDbContext>();
        optionsBuilder.UseSqlServer(connection);

        return new UserAccessDbContext(optionsBuilder.Options, eventsDispatcher: default);
    }
} 

 