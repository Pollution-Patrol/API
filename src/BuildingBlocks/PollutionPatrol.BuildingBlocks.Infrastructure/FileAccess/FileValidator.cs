namespace PollutionPatrol.BuildingBlocks.Infrastructure.FileAccess;

internal sealed class FileValidator : IFileValidator
{
    private const int MoxFileSize = 5 * 1024 * 1024; // 5MB

    private readonly IReadOnlySet<string> _allowedFileTypes = new HashSet<string>
        { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".mp4", ".avi", ".mov" };

    public bool IsSupportedFileType(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        return _allowedFileTypes.Contains(fileExtension);
    }

    public bool IsFileWeightValid(IFormFile file) => file.Length <= MoxFileSize;
}