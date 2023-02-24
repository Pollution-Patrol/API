namespace PollutionPatrol.Modules.UserAccess.Domain.RegistrationAggregate.Rules;

internal sealed class UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule : IDomainRule
{
    private readonly RegistrationStatus _status;

    public UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(RegistrationStatus status)
    {
        _status = status;
    }
    
    public string Message => "Registration should be confirmed.";

    public bool IsBroken() => _status.Value is nameof(RegistrationStatus.WaitingForConfirmation);
}