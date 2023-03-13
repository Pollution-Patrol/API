namespace PollutionPatrol.BuildingBlocks.Application.UserAccess;

public interface ICurrentUserAccessor
{
    Guid Id { get; }
    
    string Email { get; }
}