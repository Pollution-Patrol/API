namespace PollutionPatrol.BuildingBlocks.Application.Pipelines;

public class LoggingPipelineBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingPipelineBehaviour(ILogger logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Message
        _logger.Information($"Handling {typeof(TRequest).Name}");
#if DEBUG
        Type myType = request.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
        foreach (PropertyInfo prop in props)
        {
            object propValue = prop.GetValue(request, null);
            _logger.Information("{Property} : {Value}", prop.Name, propValue);
        }
#endif
        var response = await next();
        // Response
        _logger.Information($"Handled {typeof(TRequest).Name}");
        return response;
    }
}