namespace PollutionPatrol.Modules.Pollution.Application.MapConfig;

internal static class MapsterConfig
{
    internal static void AddMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Domain.ReportAggregate.Report, ReportDto>
            .NewConfig()
            .Map(dest => dest.Longitude, src => src.LocationCoordinates.Coordinate.X)
            .Map(dest => dest.Latitude, src =>  src.LocationCoordinates.Coordinate.Y)
            .Map(dest => dest.PollutionType, src => src.PollutionType.Value)
            .Map(dest => dest.ReportStatus, src => src.Status.Value);
    }
}