namespace PollutionPatrol.BuildingBlocks.Application.Options;

public sealed class ApiOptions
{
    public const string SectionName = nameof(ApiOptions);

    [Required, DataType(DataType.Url)]
    public string BaseUri { get; set; }
}