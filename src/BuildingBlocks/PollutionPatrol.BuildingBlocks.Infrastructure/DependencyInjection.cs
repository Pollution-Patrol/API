namespace PollutionPatrol.BuildingBlocks.Infrastructure;

public static class DependencyInjection
{
    public static void AddBuildingBlocksDependencyInjection(this IServiceCollection services)
    {
        services.AddMediator();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MessageValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MessageLoggingBehaviour<,>));

        services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IEmailSender, EmailSender>();
    }
}