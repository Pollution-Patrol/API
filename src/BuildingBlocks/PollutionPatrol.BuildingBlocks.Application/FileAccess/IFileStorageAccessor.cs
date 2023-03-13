namespace PollutionPatrol.BuildingBlocks.Application.FileAccess;

public interface IFileStorageAccessor
{
    Task<string> SaveFileAsync(string folderPath, string fileName, Stream fileStream);

    Task<Stream> GetFileAsync(string filePath);
}