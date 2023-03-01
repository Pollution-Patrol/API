namespace PollutionPatrol.BuildingBlocks.Application.Options;

public sealed class EmailOptions
{
    public const string SectionName = nameof(EmailOptions);

    
    [Required, DataType(DataType.EmailAddress)]
    public string Username { get; set; } 
    
    
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } 
    

    [Required]
    public string Host { get; set; }
    
    
    [Required]
    public int Port { get; set; }
}