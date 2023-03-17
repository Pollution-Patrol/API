namespace PollutionPatrol.Modules.Pollution.Application.Services;

internal static class PollutionTypeService
{
    internal static PollutionType ParseOrDefault(string? pollutionTypeString)
    {
        if (string.IsNullOrWhiteSpace(pollutionTypeString))
            return PollutionType.NotDefined;

        IReadOnlyDictionary<string, PollutionType> types = GetAllPollutionTypes();

        return types.ContainsKey(pollutionTypeString)
            ? types[pollutionTypeString]
            : PollutionType.NotDefined;
    }

    private static IReadOnlyDictionary<string, PollutionType> GetAllPollutionTypes()
    {
        Type type = typeof(PollutionType);
        IReadOnlyDictionary<string, PollutionType> pollutionTypes = type.GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => p.PropertyType == typeof(PollutionType))
            .ToDictionary(p => p.Name, f => (PollutionType)f.GetValue(null));

        return pollutionTypes;
    }
}