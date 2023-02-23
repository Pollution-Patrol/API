namespace PollutionPatrol.Modules.UserAccess.Domain.RegistrationAggregate;

public sealed class Registration : Entity, IAggregateRoot
{
    private Registration()
    {
    }

    private Registration(
        string email,
        IUserUniqueChecker userUniqueChecker,
        string passwordHash,
        string salt,
        string confirmationToken)
    {
        CheckRule(new UserEmailMustBeUniqueDomainRule(email, userUniqueChecker));

        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        ConfirmationToken = confirmationToken;
        RegisterDate = DateTime.UtcNow;
        Status = RegistrationStatus.WaitingForConfirmation;

        AddDomainEvent(new NewRegistrationCreatedDomainEvent(Email, ConfirmationToken));
    }

    public string Email { get; init; }
    public string PasswordHash { get; init; }
    public string Salt { get; init; }
    public string ConfirmationToken { get; init; }
    public DateTime RegisterDate { get; init; }
    public RegistrationStatus Status { get; private set; }

    public static Registration Create(
        string email,
        IUserUniqueChecker userUniqueChecker,
        string passwordHash,
        string salt,
        string confirmationToken)
    {
        return new(email, userUniqueChecker, passwordHash, salt, confirmationToken);
    }

    public void Confirm()
    {
        CheckRule(new RegistrationCannotBeConfirmedMoreThanOnesDomainRule(Status));

        Status = RegistrationStatus.Confirmed;
        
        AddDomainEvent(new RegistrationConfirmedDomainEvent(this));
    }

    public ApplicationUser CreateUser()
    {
        CheckRule(new UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(Status));

        return ApplicationUser.CreateFromRegistration(this);
    }
}