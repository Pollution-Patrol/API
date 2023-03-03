namespace PollutionPatrol.BuildingBlocks.Infrastructure;

public static class DependencyInjection
{
    public static void AddBuildingBlocks(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(
                Assembly.Load("PollutionPatrol.BuildingBlocks.Application"),
                Assembly.Load("PollutionPatrol.BuildingBlocks.Infrastructure"))
            .AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>))
            .AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>)));

        services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IEmailSender, EmailSender>();
    }
}