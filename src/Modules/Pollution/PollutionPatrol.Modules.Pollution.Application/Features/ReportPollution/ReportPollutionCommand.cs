namespace PollutionPatrol.Modules.Pollution.Application.Features.ReportPollution;

public sealed record ReportPollutionCommand(string PollutionType, double Longitude, double Latitude)
    : ICommand<ReportDto>;

internal sealed class ReportPollutionCommandHandler : ICommandHandler<ReportPollutionCommand, ReportDto>
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IPollutionDbContext _dbContext;

    public ReportPollutionCommandHandler(ICurrentUserAccessor currentUserAccessor, IPollutionDbContext dbContext)
    {
        _currentUserAccessor = currentUserAccessor;
        _dbContext = dbContext;
    }

    public async Task<ReportDto> Handle(ReportPollutionCommand command, CancellationToken cancellationToken)
    {
        var pollutionType = PollutionTypeService.ParseOrDefault(command.PollutionType);
        var coordinates = new Point(command.Longitude, command.Latitude);
        var userId = _currentUserAccessor.Id;

        var report = Domain.ReportAggregate.Report.Create(pollutionType, coordinates, userId);

        await _dbContext.Reports.AddAsync(report);
        await _dbContext.CommitAsync();

        var dto = new ReportDto(report.Id, report.PollutionType.Value, Longitude: coordinates.X, Latitude: coordinates.Y);
        return dto;
    }
}

internal sealed class ReportPollutionCommandValidator : AbstractValidator<ReportPollutionCommand>
{
    public ReportPollutionCommandValidator()
    {
        RuleFor(x => x.PollutionType).NotEmpty();
        RuleFor(x => x.Longitude).NotEmpty();
        RuleFor(x => x.Latitude).NotEmpty();
    }
}