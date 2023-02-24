namespace PollutionPatrol.Modules.UserAccess.Domain.RegistrationAggregate;

public sealed class RegistrationStatus : ValueObject
{
    private RegistrationStatus(string value) => Value = value;

    public string Value { get; init; }
    
    public static RegistrationStatus WaitingForConfirmation => new(nameof(WaitingForConfirmation));
    public static RegistrationStatus Confirmed => new(nameof(Confirmed));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}