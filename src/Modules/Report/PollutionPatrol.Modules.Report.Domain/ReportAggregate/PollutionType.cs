namespace PollutionPatrol.Modules.Report.Domain.ReportAggregate;

public sealed class PollutionType : ValueObject
{
    private PollutionType(string value) => Value = value;

    public string Value { get; init; }

    public static PollutionType SoilPollution => new(nameof(SoilPollution));
    public static PollutionType AirPollution => new(nameof(AirPollution));
    public static PollutionType PlasticPollution => new(nameof(PlasticPollution));
    public static PollutionType MicroplasticPollution => new(nameof(MicroplasticPollution));
    public static PollutionType WatterPollution => new(nameof(WatterPollution));
    public static PollutionType NoisePollution => new(nameof(NoisePollution));
    public static PollutionType LightPollution => new(nameof(LightPollution));
    public static PollutionType RadioactivePollution => new(nameof(RadioactivePollution));
    public static PollutionType ThermalPollution => new(nameof(ThermalPollution));
    public static PollutionType VisualPollution => new(nameof(VisualPollution));

    /// <summary>
    /// Represents a pollution type that is not defined.
    /// </summary>
    /// <remarks>
    /// This pollution type is used when none of the other pollution types match the report.
    /// </remarks>
    public static PollutionType NotDefined => new(nameof(NotDefined));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}