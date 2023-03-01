namespace PollutionPatrol.BuildingBlocks.Application.Options;

public sealed class ApiOptions
{
    [Required, DataType(DataType.Url)]
    public string BaseUri { get; set; }
}