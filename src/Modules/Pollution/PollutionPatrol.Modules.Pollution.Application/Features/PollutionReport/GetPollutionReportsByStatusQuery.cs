namespace PollutionPatrol.Modules.Pollution.Application.Features.PollutionReport;

public sealed record GetPollutionReportsByStatusQuery(string? ReportStatus, int Page, int Size) :
    IQuery<ReadOnlyPagedList<ReportDto>>, IPagedQuery;

internal sealed class GetPollutionReportsByStatusQueryHandler :
    IQueryHandler<GetPollutionReportsByStatusQuery, ReadOnlyPagedList<ReportDto>>
{
    private readonly IPollutionDbContext _dbContext;

    public GetPollutionReportsByStatusQueryHandler(IPollutionDbContext dbContext) => _dbContext = dbContext;

    public async Task<ReadOnlyPagedList<ReportDto>> Handle(GetPollutionReportsByStatusQuery query, CancellationToken cancellationToken)
    {
        
        var reportDtos = await _dbContext.Reports
            .Where(r => r.Status.Value.Equals(query.ReportStatus))
            .ProjectToType<ReportDto>()
            .ToReadOnlyPagedListAsync(query.Page, query.Size);

        return reportDtos;
    }
}

internal sealed class GetPollutionReportsByStatusQueryValidator : AbstractValidator<GetPollutionReportsByStatusQuery>
{
    public GetPollutionReportsByStatusQueryValidator()
    {
        RuleFor(x => x.Page).NotEmpty();
        RuleFor(x => x.Size).NotEmpty();
        RuleFor(x => x.ReportStatus)
            .NotEmpty()
            .Must(ReportStatusService.IsDefined)
            .WithMessage("The status you provided is not valid. Please make sure to select a status" +
                         " from the available options:" +
                         " NotCompleted, Pending, Reviewing, Approved, or Rejected.");
    }
}