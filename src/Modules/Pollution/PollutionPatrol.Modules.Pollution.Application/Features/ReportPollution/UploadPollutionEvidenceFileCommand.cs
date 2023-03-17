namespace PollutionPatrol.Modules.Pollution.Application.Features.ReportPollution;

public sealed record UploadPollutionEvidenceFileCommand(Guid ReportId, IFormFile EvidenceFile) : ICommand<ReportDto>;

internal sealed class UploadEvidenceFileCommandHandler : ICommandHandler<UploadPollutionEvidenceFileCommand, ReportDto>
{
    private readonly IFileStorageAccessor _fileStorageAccessor;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IPollutionDbContext _dbContext;

    public UploadEvidenceFileCommandHandler(
        IFileStorageAccessor fileStorageAccessor,
        ICurrentUserAccessor currentUserAccessor,
        IPollutionDbContext dbContext)
    {
        _fileStorageAccessor = fileStorageAccessor;
        _currentUserAccessor = currentUserAccessor;
        _dbContext = dbContext;
    }

    public async Task<ReportDto> Handle(UploadPollutionEvidenceFileCommand command, CancellationToken cancellationToken)
    {
        var report = await _dbContext.Reports.FirstOrDefaultAsync(r => r.Id.Equals(command.ReportId));

        if (report is null)
            throw new SpecNotFoundException(nameof(Report));

        var userId = _currentUserAccessor.Id;
        var folderName = $"{userId}/{report.Id}";
        var fileName = $"{Guid.NewGuid()}";
        await using var fileStream = command.EvidenceFile.OpenReadStream();

        var evidenceFileKey = await _fileStorageAccessor.SaveFileAsync(folderName, fileName, fileStream);
        report.SetEvidenceFileKey(evidenceFileKey);

        _dbContext.Reports.Update(report);
        await _dbContext.CommitAsync();

        var dto = report.Adapt<ReportDto>();
        return dto;
    }
}

internal sealed class UploadEvidenceFileCommandValidator : AbstractValidator<UploadPollutionEvidenceFileCommand>
{
    public UploadEvidenceFileCommandValidator(IFileValidator fileValidator, IVideoValidator videoValidator)
    {
        RuleFor(x => x.ReportId).NotEmpty();
        RuleFor(x => x.EvidenceFile)
            .NotNull().WithMessage("No file was selected.")
            .NotEmpty().WithMessage("The selected file is empty.")
            .Must(fileValidator.IsSupportedFileType).WithMessage("Only image and video files are allowed.")
            .Must(fileValidator.IsFileWeightValid).WithMessage("The file size must be less than 20MB.")
            .MustAsync(async (file, token) => await videoValidator.IsShorterThan15SecondsAsync(file)).WithMessage("The video duration must be less than 15 seconds.");
    }
}