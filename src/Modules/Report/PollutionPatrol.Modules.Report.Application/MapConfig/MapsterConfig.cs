namespace PollutionPatrol.Modules.Report.Application.MapConfig;

internal static class MapsterConfig
{
    internal static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Domain.ReportAggregate.Report, ReportDto>
            .NewConfig()
            .Map(dest => dest.Longitude, src => src.LocationCoordinates.X)
            .Map(dest => dest.Longitude, src => src.LocationCoordinates.X)
            .Map(dest => dest.PollutionType, src => src.PollutionType.Value);
    }
}