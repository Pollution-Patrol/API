namespace PollutionPatrol.Modules.Pollution.Domain.ReportAggregate;

public sealed class ReportStatus : ValueObject
{
    private ReportStatus(string value) => Value = value;

    public string Value { get; init; }

    /// <summary>
    /// Represents a report that has not been completed.
    /// </summary>
    /// <remarks>
    /// This report status indicates that the report has not been fully completed and requires an evidence file to be provided.
    /// </remarks>
    public static ReportStatus NotCompleted => new(nameof(NotCompleted));

    /// <summary>
    /// Represents a report that has been completed but not yet taken on review.
    /// </summary>
    /// <remarks>
    /// This report status indicates that the report has been completed and an evidence file has been provided,
    /// but it has not yet been reviewed by a designated reviewer.
    /// </remarks>
    public static ReportStatus Pending => new(nameof(Pending));

    /// <summary>
    /// Represents a report that is currently being reviewed.
    /// </summary>
    /// <remarks>
    /// This report status indicates that the report is being reviewed by a designated reviewer.
    /// </remarks>
    public static ReportStatus Reviewing => new(nameof(Reviewing));

    /// <summary>
    /// Represents a report that has been approved.
    /// </summary>
    /// <remarks>
    /// This report status indicates that the report has been reviewed and approved by a designated reviewer.
    /// </remarks>
    public static ReportStatus Approved => new(nameof(Approved));

    /// <summary>
    /// Represents a report that has been rejected.
    /// </summary>
    /// <remarks>
    /// This report status indicates that the report has been reviewed but rejected by a designated reviewer.
    /// </remarks>
    public static ReportStatus Rejected => new(nameof(Rejected));


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}