namespace PollutionPatrol.BuildingBlocks.Application.Pipelines;

public class LoggingPipelineBehaviour<TMessage, TResponse> :
    IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly ILogger _logger;

    public LoggingPipelineBehaviour(ILogger logger) => _logger = logger;

    public async ValueTask<TResponse> Handle(
        TMessage message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        // Message
        _logger.Information($"Handling {typeof(TMessage).Name}");
#if DEBUG
        Type myType = message.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
        foreach (PropertyInfo prop in props)
        {
            object propValue = prop.GetValue(message, null);
            _logger.Information("{Property} : {Value}", prop.Name, propValue);
        }
#endif
        var response = await next(message, cancellationToken);
        // Response
        _logger.Information($"Handled {typeof(TMessage).Name}");
        return response;
    }
}