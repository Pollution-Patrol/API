namespace PollutionPatrol.Modules.Report.Domain.ReportAggregate;

public interface IReviewerAssignment
{
    bool IsReviewerAvailable(Guid reviewerId);
}