namespace PollutionPatrol.Modules.Pollution.Application.Features.ReviewReport;

public sealed record RejectPollutionReportCommand(Guid ReportId) : ICommand<ReportDto>;

internal sealed class RejectPollutionReportCommandCommandHandler : ICommandHandler<ApprovePollutionReportCommand, ReportDto>
{
    private readonly IPollutionDbContext _dbContext;

    public RejectPollutionReportCommandCommandHandler(IPollutionDbContext dbContext) => _dbContext = dbContext;

    public async Task<ReportDto> Handle(ApprovePollutionReportCommand command, CancellationToken cancellationToken)
    {
        var report = await _dbContext.Reports.FirstOrDefaultAsync(r => r.Id.Equals(command.ReportId));

        if (report is null)
            throw new SpecNotFoundException(nameof(Report));

        report.Reject();

        _dbContext.Reports.Update(report);
        await _dbContext.CommitAsync();

        return report.Adapt<ReportDto>();
    }
}

internal sealed class RejectPollutionReportCommandValidator : AbstractValidator<ApprovePollutionReportCommand>
{
    public RejectPollutionReportCommandValidator()
    {
        RuleFor(x => x.ReportId).NotEmpty();
    }
}