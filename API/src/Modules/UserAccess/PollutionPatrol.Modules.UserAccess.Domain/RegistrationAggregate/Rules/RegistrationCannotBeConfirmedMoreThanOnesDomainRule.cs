namespace PollutionPatrol.Modules.UserAccess.Domain.RegistrationAggregate.Rules;

internal sealed class RegistrationCannotBeConfirmedMoreThanOnesDomainRule : IDomainRule
{
    private readonly RegistrationStatus _status;

    public RegistrationCannotBeConfirmedMoreThanOnesDomainRule(RegistrationStatus status) =>
        _status = status;

    public string Message => "Registration is already confirmed.";

    public bool IsBroken() => _status.Value is nameof(RegistrationStatus.Confirmed);
}