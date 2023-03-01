namespace PollutionPatrol.Modules.UserAccess.Infrastructure;

public static class DependencyInjection
{
    public static void AddUserAccessModule(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("UserAccess");

        services.AddDbContext<UserAccessDbContext>(options =>
            options.UseSqlServer(connection));

        services.AddScoped<IUserAccessDbContext, UserAccessDbContext>();
        services.AddScoped<IUserUniqueChecker, UserUniqueChecker>();
        services.AddScoped<IPasswordManager, PasswordManager>();
        services.AddScoped<ITokenClaimsService, TokenClaimsService>();
    }
}