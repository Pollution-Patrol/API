namespace PollutionPatrol.Modules.UserAccess.Domain.UserAggregate.Events;

public sealed class NewApplicationUserRegisteredDomainEvent : IDomainEvent
{
    public NewApplicationUserRegisteredDomainEvent(string email)
    {
        Email = email;
    }

    public string Email { get; init; }
}