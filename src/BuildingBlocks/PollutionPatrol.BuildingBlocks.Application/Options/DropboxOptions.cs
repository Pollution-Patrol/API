namespace PollutionPatrol.BuildingBlocks.Application.Options;

public sealed class DropboxOptions
{
    public const string SectionName = nameof(DropboxOptions);
    
    [Required]
    public string Appkey { get; set; }


    [Required]
    public string AppSecret { get; set; }


    [Required]
    public string OAuthToken { get; set; }
}