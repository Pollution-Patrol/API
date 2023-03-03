namespace PollutionPatrol.Modules.UserAccess.Application.Features.User;

internal sealed class AddNewUserCommand : IDomainEventHandler<RegistrationConfirmedDomainEvent>
{
    private readonly IUserAccessDbContext _dbContext;

    public AddNewUserCommand(IUserAccessDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(RegistrationConfirmedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var user = domainEvent.Registration.CreateUser();

        await _dbContext.Users.AddAsync(user);
        await _dbContext.CommitAsync();
    }
}