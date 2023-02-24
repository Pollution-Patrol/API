namespace PollutionPatrol.Modules.UserAccess.Domain.RegistrationAggregate;

public interface IUserUniqueChecker
{
    bool IsEmailUnique(string email);
}