namespace PollutionPatrol.BuildingBlocks.Application.FileAccess;

public interface IVideoValidator
{
    Task<bool> IsShorterThan15SecondsAsync(IFormFile file);
}