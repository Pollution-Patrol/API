namespace PollutionPatrol.Modules.UserAccess.Application.Features.UserRegistration;

public sealed record ConfirmRegistrationCommand(string ConfirmationToken) : ICommand;

public sealed class ConfirmRegistrationCommandHandler : ICommandHandler<ConfirmRegistrationCommand>
{
    private readonly IUserAccessDbContext _accessDbContext;

    public ConfirmRegistrationCommandHandler(IUserAccessDbContext accessDbContext) => _accessDbContext = accessDbContext;

    public async Task Handle(ConfirmRegistrationCommand command, CancellationToken cancellationToken)
    {
        var registration = await _accessDbContext.Registrations
            .FirstOrDefaultAsync(x => x.ConfirmationToken.Equals(command.ConfirmationToken));

        if (registration is null)
            throw new SpecNotFoundException(nameof(Registration));

        registration.Confirm();

        _accessDbContext.Registrations.Update(registration);
        await _accessDbContext.CommitAsync();
    }
}

public sealed class ConfirmRegistrationCommandValidator : AbstractValidator<ConfirmRegistrationCommand>
{
    public ConfirmRegistrationCommandValidator()
    {
        RuleFor(x => x.ConfirmationToken).NotEmpty();
    }
} 