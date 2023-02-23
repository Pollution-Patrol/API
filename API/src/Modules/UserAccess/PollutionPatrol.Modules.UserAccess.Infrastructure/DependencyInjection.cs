namespace PollutionPatrol.Modules.UserAccess.Infrastructure;

public static class DependencyInjection
{
    public static void AddUserAccessModule(this IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connection = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<UserAccessDbContext>(options =>
            options.UseSqlServer(connection));

        services.AddScoped<IUserAccessDbContext, UserAccessDbContext>();
        services.AddScoped<IUserUniqueChecker, UserUniqueChecker>();
    }
}