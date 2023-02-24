namespace PollutionPatrol.Modules.UserAccess.Domain.RegistrationAggregate.Rules;

internal sealed class UserEmailMustBeUniqueDomainRule : IDomainRule
{
    private readonly string _email;
    private readonly IUserUniqueChecker _userUniqueChecker;

    public UserEmailMustBeUniqueDomainRule(string email, IUserUniqueChecker userUniqueChecker)
    {
        _email = email;
        _userUniqueChecker = userUniqueChecker;
    }

    public string Message => "The email is already taken. User email must be unique.";

    public bool IsBroken() => _userUniqueChecker.IsEmailUnique(_email) is false;
}