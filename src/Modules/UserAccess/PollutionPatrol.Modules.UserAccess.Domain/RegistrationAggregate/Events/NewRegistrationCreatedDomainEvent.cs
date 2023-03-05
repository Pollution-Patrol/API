namespace PollutionPatrol.Modules.UserAccess.Domain.RegistrationAggregate.Events;

public sealed class NewRegistrationCreatedDomainEvent : IDomainEvent
{
    public NewRegistrationCreatedDomainEvent(string userEmail, string confirmationToken)
    {
        UserEmail = userEmail;
        ConfirmationToken = confirmationToken;
    }

    public string UserEmail { get; }
    public string ConfirmationToken { get; }
}