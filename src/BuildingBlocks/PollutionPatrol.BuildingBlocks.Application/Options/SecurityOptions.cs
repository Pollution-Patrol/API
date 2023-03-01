namespace PollutionPatrol.BuildingBlocks.Application.Options;

public sealed class SecurityOptions
{
    public const string SectionName = nameof(SecurityOptions);

    
    [Required]
    public string JwtSecretKey { get; set; }
}