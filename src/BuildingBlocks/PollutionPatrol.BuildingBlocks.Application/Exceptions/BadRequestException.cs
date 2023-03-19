namespace PollutionPatrol.BuildingBlocks.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string details, string? message = default) : base(message)
    {
        Details = details;
    }
    
    /// <summary>
    /// Gets the exception details for use in problem details.
    /// </summary>
    public string Details { get; }

    public override string ToString() => $"Bad is invalid: {Message}.";
}