namespace PollutionPatrol.Modules.UserAccess.Domain.RegistrationAggregate.Events;

public sealed class RegistrationConfirmedDomainEvent : IDomainEvent
{
    public RegistrationConfirmedDomainEvent(Registration registration)
    {
        Registration = registration;
    }

    public Registration Registration { get; init; }
}