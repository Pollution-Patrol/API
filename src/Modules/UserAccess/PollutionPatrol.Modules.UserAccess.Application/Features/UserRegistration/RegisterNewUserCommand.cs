namespace PollutionPatrol.Modules.UserAccess.Application.Features.UserRegistration;

public sealed record RegisterNewUserCommand(string Email, string Password) : ICommand;

internal sealed class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand>
{
    private readonly IUserUniqueChecker _userUniqueChecker;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserAccessDbContext _dbContext;

    public RegisterNewUserCommandHandler(
        IUserUniqueChecker userUniqueChecker,
        IPasswordManager passwordManager,
        IUserAccessDbContext dbContext)
    {
        _userUniqueChecker = userUniqueChecker;
        _passwordManager = passwordManager;
        _dbContext = dbContext;
    }

    public async Task Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
    {
        var salt = _passwordManager.GenerateSalt();
        var hash = await _passwordManager.HashPasswordAsync(command.Password, salt);
        var confirmToken = $"{Guid.NewGuid()}";

        var registration = Registration.Create(
            command.Email,
            _userUniqueChecker,
            Convert.ToHexString(hash),
            Convert.ToHexString(salt),
            confirmToken);

        await _dbContext.Registrations.AddAsync(registration);
        await _dbContext.CommitAsync();
    }
}

internal sealed class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
{
    public RegisterNewUserCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(6);
    }
}