namespace PollutionPatrol.BuildingBlocks.Infrastructure.FileAccess;

internal sealed class DropboxFileStorageAccessor : IFileStorageAccessor
{
    private readonly DropboxOptions _dropboxOptions;

    public DropboxFileStorageAccessor(IOptions<DropboxOptions> options) => _dropboxOptions = options.Value;

    public async Task<string> SaveFileAsync(string folderPath, string fileName, Stream fileStream)
    {
        using var client = new DropboxClient(_dropboxOptions.OAuthToken);

        await CreateFolderIfNotExists(folderPath, client);

        var uploadPath = $"/{folderPath}/{fileName}";

        var fileMetadata = await client.Files.UploadAsync(
            uploadPath,
            mode: WriteMode.Overwrite.Instance,
            body: fileStream);

        return fileMetadata.PathLower;
    }

    public async Task<Stream> GetFileAsync(string filePath)
    {
        using var client = new DropboxClient(_dropboxOptions.OAuthToken);

        using var downloadResponse = await client.Files.DownloadAsync(filePath);

        return await downloadResponse.GetContentAsStreamAsync();
    }

    private async Task CreateFolderIfNotExists(string folderPath, DropboxClient client)
    {
        try
        {
            var metadata = await client.Files.GetMetadataAsync($"/{folderPath}");
            if (!metadata.IsFolder || metadata.IsDeleted)
                await client.Files.CreateFolderV2Async($"/{folderPath}");
        }
        catch (DropboxException ex) when (ex.Message.Contains("path/not_found/"))
        {
            await client.Files.CreateFolderV2Async($"/{folderPath}");
        }
    }
}