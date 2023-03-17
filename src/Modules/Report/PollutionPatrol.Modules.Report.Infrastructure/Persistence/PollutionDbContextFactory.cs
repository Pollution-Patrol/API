namespace PollutionPatrol.Modules.Report.Infrastructure.Persistence;

internal sealed class PollutionDbContextFactory : IDesignTimeDbContextFactory<ReportDbContext>
{
    public ReportDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "API", "PollutionPatrol.API"))
            .AddJsonFile("appsettings.json")
            .Build();
        
        var connection = config.GetConnectionString("Report");

        var optionsBuilder = new DbContextOptionsBuilder<ReportDbContext>();
        optionsBuilder.UseNpgsql(connection, x => x.UseNetTopologySuite());

        return new ReportDbContext(optionsBuilder.Options, default);
    }
}