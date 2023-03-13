namespace PollutionPatrol.BuildingBlocks.Infrastructure.FileAccess;

internal sealed class VideoValidator : IVideoValidator
{
    private const int MaxTotalVideoSeconds = 15;

    public async Task<bool> IsShorterThan15SecondsAsync(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (fileExtension is ".mp4" or ".mov")
        {
            using var memStream = new MemoryStream();
            await file.CopyToAsync(memStream);
            memStream.Position = 0;
            var videoInfo = await FFProbe.AnalyseAsync(memStream);
            return videoInfo.Duration.TotalSeconds < MaxTotalVideoSeconds;
        }

        return true;
    }
}