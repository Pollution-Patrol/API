namespace PollutionPatrol.BuildingBlocks.Application.FileAccess;

public interface IFileValidator
{
    bool IsSupportedFileType(IFormFile file);

    bool IsFileWeightValid(IFormFile file);
}