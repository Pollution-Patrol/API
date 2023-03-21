namespace PollutionPatrol.Modules.Pollution.Application.Features.PollutionReport;

public sealed record GetPollutionReportsQuery(int Page, int Size) : IQuery<ReadOnlyPagedList<ReportDto>>, IPagedQuery;

internal sealed class GetPollutionReportsQueryHandler :
    IQueryHandler<GetPollutionReportsQuery, ReadOnlyPagedList<ReportDto>>
{
    private readonly IPollutionDbContext _dbContext;

    public GetPollutionReportsQueryHandler(IPollutionDbContext dbContext) => _dbContext = dbContext;

    public async Task<ReadOnlyPagedList<ReportDto>> Handle(GetPollutionReportsQuery query, CancellationToken cancellationToken)
    {
        var reportDtosList = await _dbContext.Reports
            .ProjectToType<ReportDto>()
            .ToReadOnlyPagedListAsync(query.Page, query.Size);

        return reportDtosList;
    }
}

internal sealed class GetPollutionReportsQueryValidator : AbstractValidator<GetPollutionReportsQuery>
{
    public GetPollutionReportsQueryValidator()
    {
        RuleFor(x => x.Page).NotEmpty();
        RuleFor(x => x.Size).NotEmpty();
    }
}