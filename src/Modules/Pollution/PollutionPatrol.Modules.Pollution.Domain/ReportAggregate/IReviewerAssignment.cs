namespace PollutionPatrol.Modules.Pollution.Domain.ReportAggregate;

public interface IReviewerAssignment
{
    bool IsReviewerAvailable(Guid reviewerId);
}