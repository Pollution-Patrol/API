namespace PollutionPatrol.Modules.Pollution.Application.Features.ReviewReport;

public sealed record ApprovePollutionReportCommand(Guid ReportId) : ICommand<ReportDto>;

internal sealed class ApprovePollutionReportCommandHandler : ICommandHandler<ApprovePollutionReportCommand, ReportDto>
{
    private readonly IPollutionDbContext _dbContext;

    public ApprovePollutionReportCommandHandler(IPollutionDbContext dbContext) => _dbContext = dbContext;

    public async Task<ReportDto> Handle(ApprovePollutionReportCommand command, CancellationToken cancellationToken)
    {
        var report = await _dbContext.Reports.FirstOrDefaultAsync(r => r.Id.Equals(command.ReportId));

        if (report is null)
            throw new SpecNotFoundException(nameof(Report));

        report.Approve();

        _dbContext.Reports.Update(report);
        await _dbContext.CommitAsync();

        return report.Adapt<ReportDto>();
    }
}

internal sealed class ApprovePollutionReportCommandValidator : AbstractValidator<ApprovePollutionReportCommand>
{
    public ApprovePollutionReportCommandValidator()
    {
        RuleFor(x => x.ReportId).NotEmpty();
    }
}