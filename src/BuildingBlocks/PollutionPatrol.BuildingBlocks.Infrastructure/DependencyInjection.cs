namespace PollutionPatrol.BuildingBlocks.Infrastructure;

public static class DependencyInjection
{
    public static void AddBuildingBlocks(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(
            Assembly.Load("PollutionPatrol.BuildingBlocks.Application")));

        services.AddTransient<IFileStorageAccessor, DropboxFileStorageAccessor>();
        services.AddTransient<IFileValidator, FileValidator>();
        services.AddTransient<IVideoValidator, VideoValidator>();
        services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IEmailSender, EmailSender>();
    }
}