namespace PollutionPatrol.Modules.Pollution.Application.Features.PollutionReport;

public sealed record GetPollutionReportsByUserIdQuery(Guid UserId, int Page, int Size) : 
    IQuery<ReadOnlyPagedList<ReportDto>>, IPagedQuery;

internal sealed class GetPollutionReportsByUserIdQueryHandler :
    IQueryHandler<GetPollutionReportsByUserIdQuery, ReadOnlyPagedList<ReportDto>>
{
    private readonly IPollutionDbContext _dbContext;

    public GetPollutionReportsByUserIdQueryHandler(IPollutionDbContext dbContext) => _dbContext = dbContext;

    public async Task<ReadOnlyPagedList<ReportDto>> Handle(GetPollutionReportsByUserIdQuery query, CancellationToken cancellationToken)
    {
        var reportsDtos = await _dbContext.Reports
            .Where(r => r.UserId.Equals(query.UserId))
            .ProjectToType<ReportDto>()
            .ToReadOnlyPagedListAsync(query.Page, query.Size);

        return reportsDtos;
    }
}

internal sealed class GetPollutionReportsByUserIdQueryValidator :
    AbstractValidator<GetPollutionReportsByUserIdQuery>
{
    public GetPollutionReportsByUserIdQueryValidator()
    {
        RuleFor(x => x.Page).NotEmpty();
        RuleFor(x => x.Size).NotEmpty();
    }
}