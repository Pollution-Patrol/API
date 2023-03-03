namespace PollutionPatrol.BuildingBlocks.Application.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException(string details, string? message = default) : base(message)
    {
        Details = details;
    }

    public AuthenticationException(string? message = default) : base(message)
    {
    }

    public string Details { get; }
    
    public override string ToString() => $"User is not authenticated: {Message}";
}