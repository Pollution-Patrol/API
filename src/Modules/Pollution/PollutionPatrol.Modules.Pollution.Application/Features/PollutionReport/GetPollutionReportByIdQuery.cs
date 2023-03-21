namespace PollutionPatrol.Modules.Pollution.Application.Features.PollutionReport;

public sealed record GetPollutionReportByIdQuery(Guid ReportId) : IQuery<ReportDto>;

internal sealed class GetPollutionReportByIdQueryHandler : IQueryHandler<GetPollutionReportByIdQuery, ReportDto>
{
    private readonly IPollutionDbContext _dbContext;

    public GetPollutionReportByIdQueryHandler(IPollutionDbContext dbContext) => _dbContext = dbContext;

    public async Task<ReportDto> Handle(GetPollutionReportByIdQuery query, CancellationToken cancellationToken)
    {
        var report = await _dbContext.Reports.FirstOrDefaultAsync(r => r.Id.Equals(query.ReportId));

        if (report is null)
            throw new SpecNotFoundException(nameof(Report));

        return report.Adapt<ReportDto>();
    }
}

internal sealed class GetPollutionReportByIdQueryValidator : AbstractValidator<GetPollutionReportByIdQuery>
{
    public GetPollutionReportByIdQueryValidator()
    {
        RuleFor(x => x.ReportId).NotEmpty();
    }
}