namespace PollutionPatrol.Modules.Pollution.Application.Features.ReviewReport;

public sealed record DesignateReportReviewerCommand(Guid ReportId, Guid ReviewerId) : ICommand<ReportDto>;

internal sealed class DesignateReportReviewerCommandHandler : ICommandHandler<DesignateReportReviewerCommand, ReportDto>
{
    private readonly IReviewerAssignment _reviewerAssignment;
    private readonly IPollutionDbContext _dbContext;

    public DesignateReportReviewerCommandHandler(IReviewerAssignment reviewerAssignment, IPollutionDbContext dbContext)
    {
        _reviewerAssignment = reviewerAssignment;
        _dbContext = dbContext;
    }

    public async Task<ReportDto> Handle(DesignateReportReviewerCommand command, CancellationToken cancellationToken)
    {
        var report = await _dbContext.Reports.FirstOrDefaultAsync(r => r.Id.Equals(command.ReportId));

        if (report is null)
            throw new SpecNotFoundException(nameof(report));

        report.DesignateReviewer(_reviewerAssignment, command.ReviewerId);

        _dbContext.Reports.Update(report);
        await _dbContext.CommitAsync();
        
        return report.Adapt<ReportDto>();
    }
}

internal sealed class DesignateReportReviewerCommandValidator : AbstractValidator<DesignateReportReviewerCommand>
{
    public DesignateReportReviewerCommandValidator()
    {
        RuleFor(x => x.ReportId).NotEmpty();
        RuleFor(x => x.ReviewerId).NotEmpty();
    }
}