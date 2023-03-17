namespace PollutionPatrol.Modules.Report.Infrastructure;

public static class DependencyInjection
{
    public static void AddReportModule(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("Report");

        services.AddDbContext<ReportDbContext>(options =>
            options.UseNpgsql(connection, x => x.UseNetTopologySuite()));

        services.AddMediatR(x => x.RegisterServicesFromAssemblies(
                Assembly.Load("PollutionPatrol.Modules.Report.Application"),
                Assembly.Load("PollutionPatrol.Modules.Report.Infrastructure"))
            .AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>))
            .AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>)));

        services.AddValidatorsFromAssembly(Assembly.Load("PollutionPatrol.Modules.Report.Application"), includeInternalTypes: true);
        
        services.RegisterMapsterConfiguration();
        
        services.AddTransient<IReportModule, ReportModule>();
        services.AddScoped<IReportDbContext, ReportDbContext>();
    }
}