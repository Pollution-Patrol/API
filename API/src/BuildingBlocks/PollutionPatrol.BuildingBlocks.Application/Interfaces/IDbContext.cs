namespace PollutionPatrol.BuildingBlocks.Application.Interfaces;

public interface IDbContext
{
    Task CommitAsync();
}