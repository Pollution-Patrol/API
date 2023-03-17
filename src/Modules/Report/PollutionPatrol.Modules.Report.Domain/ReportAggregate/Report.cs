namespace PollutionPatrol.Modules.Report.Domain.ReportAggregate;

/// <summary>
/// Represents a pollution report submitted by a user.
/// </summary>
public sealed class Report : Entity, IAggregateRoot
{
    private Report(PollutionType pollutionType, Point locationCoordinates, Guid userId)
    {
        PollutionType = pollutionType;
        LocationCoordinates = locationCoordinates;
        UserId = userId;
        Status = ReportStatus.NotCompleted;
        ReportDate = DateTime.UtcNow;
    }

    private Report()
    {
        // EF only
    }

    public PollutionType PollutionType { get; private set; }
    public Point LocationCoordinates { get; init; }
    public string? EvidenceFileKey { get; private set; }
    public ReportStatus Status { get; private set; }
    public DateTime ReportDate { get; init; }
    public Guid UserId { get; init; }
    public Guid? DesignatedReviewerId { get; private set; }

    /// <summary>
    /// Creates a new <see cref="Report"/> instance.
    /// </summary>
    /// <param name="pollutionType">The type of pollution being reported.</param>
    /// <param name="location">The GPS coordinates of the pollution location.</param>
    /// <param name="userId">The ID of the user who submitted the report.</param>
    /// <returns>A new <see cref="Report"/> instance.</returns>
    public static Report Create(PollutionType pollutionType, Point location, Guid userId)
    {
        return new(pollutionType, location, userId);
    }

    /// <summary>
    /// Sets the evidence file key associated with the report.
    /// </summary>
    /// <param name="evidenceFileKey">The key of the evidence file.</param>
    /// <exception cref="ReportEvidenceFileKeyCanBeSetOnlyOnceDomainRule">
    /// Thrown if the evidence file key has already been set.
    /// </exception>
    public void SetEvidenceFileKey(string evidenceFileKey)
    {
        CheckRule(new ReportEvidenceFileKeyCanBeSetOnlyOnceDomainRule(EvidenceFileKey));

        EvidenceFileKey = evidenceFileKey;
        
        AddDomainEvent(new ReportPollutionEvidenceFileHasBeenSetDomainEvent(this));
    }

    /// <summary>
    /// Designates a reviewer for the report.
    /// </summary>
    /// <param name="reviewerAssignment">The assignment strategy for the reviewer.</param>
    /// <param name="reviewerId">The ID of the designated reviewer.</param>
    /// <exception cref="ReportReviewerCanBeChangedDuringPendingStatusOnlyDomainRule">
    /// Thrown if the report status is not <see cref="ReportStatus.NotCompleted"/>.
    /// </exception>
    /// <exception cref="ReviewerCannotHaveMoreThanOneReportAtOneTimeDomainRule">
    /// Thrown if the reviewer has already been assigned to another report.
    /// </exception>
    /// <remarks>
    /// This method sets the designated reviewer for the report and changes the report status to <see cref="ReportStatus.Reviewing"/>.
    /// </remarks>
    public void DesignateReviewer(IReviewerAssignment reviewerAssignment, Guid reviewerId)
    {
        CheckRule(new ReportReviewerCanBeChangedDuringPendingStatusOnlyDomainRule(Status));
        CheckRule(new ReviewerCannotHaveMoreThanOneReportAtOneTimeDomainRule(reviewerAssignment, reviewerId));

        DesignatedReviewerId = reviewerId;
        Status = ReportStatus.Reviewing;
    }

    /// <summary>
    /// Marks the report as complete and ready for review.
    /// </summary>
    /// <remarks>
    /// This method should be called after an evidence file has been attached to the report
    /// and the report is ready for review by a designated reviewer.
    /// </remarks>
    public void Complete()
    {
        CheckRule(new ReportCannotBeCompletedTwiceDomainRule(Status));
        CheckRule(new ReportCannotBeCompletedIfEvidenceFileKeyHasNotBeenSetDomainRule(EvidenceFileKey));

        Status = ReportStatus.Pending;
    }

    /// <summary>
    /// Approves the report.
    /// </summary>
    /// <exception cref="ReportCanBeApprovedOnlyInReviewingStatusDomainRule">
    /// Thrown if the report status is not <see cref="ReportStatus.Reviewing"/>.
    /// </exception>
    /// <remarks>
    /// This method changes the report status to <see cref="ReportStatus.Approved"/> upon successful approval.
    /// </remarks>
    public void Approve()
    {
        CheckRule(new ReportCanBeApprovedOnlyInReviewingStatusDomainRule(Status));

        Status = ReportStatus.Approved;
    }

    /// <summary>
    /// Rejects the report.
    /// </summary>
    /// <exception cref="ReportCanBeRejectedOnlyInReviewingStatusDomainRule">
    /// Thrown if the report status is not <see cref="ReportStatus.Reviewing"/>.
    /// </exception>
    /// <remarks>
    /// This method changes the report status to <see cref="ReportStatus.Rejected"/> upon rejection.
    /// </remarks>
    public void Reject()
    {
        CheckRule(new ReportCanBeRejectedOnlyInReviewingStatusDomainRule(Status));

        Status = ReportStatus.Rejected;
    }
}