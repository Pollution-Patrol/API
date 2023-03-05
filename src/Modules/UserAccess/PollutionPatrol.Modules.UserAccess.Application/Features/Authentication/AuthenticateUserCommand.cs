namespace PollutionPatrol.Modules.UserAccess.Application.Features.Authentication;

public sealed record AuthenticateUserCommand(string Email, string Password) : ICommand<AuthenticationResult>;

internal sealed class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, AuthenticationResult>
{
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserAccessDbContext _dbContext;

    public AuthenticateUserCommandHandler(
        ITokenClaimsService tokenClaimsService,
        IPasswordManager passwordManager,
        IUserAccessDbContext dbContext)
    {
        _tokenClaimsService = tokenClaimsService;
        _passwordManager = passwordManager;
        _dbContext = dbContext;
    }

    public async Task<AuthenticationResult> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(command.Email));

        if (user is null)
            throw new AuthenticationException(details: "Authentication failed. The email you entered is incorrect. Please try again.");

        var isVerified = await _passwordManager.VerifyPasswordAsync(command.Password, Convert.FromHexString(user.PasswordHash),
            Convert.FromHexString(user.Salt));
        if (isVerified is false)
            throw new AuthenticationException(details: "Authentication failed. The password you entered is incorrect. Please try again.");

        var token = _tokenClaimsService.GenerateToken(user);
        return new AuthenticationResult(token);
    }
}

internal sealed class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(6);
    }
}