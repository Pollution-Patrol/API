namespace PollutionPatrol.BuildingBlocks.Application.Pipelines;

public class MessageLoggingBehaviour<TMessage, TResponse> :
    IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly ILogger _logger;

    public async ValueTask<TResponse> Handle(
        TMessage message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        //Message
        _logger.Information($"Handling {typeof(TMessage).Name}");
        Type myType = message.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
        foreach (PropertyInfo prop in props)
        {
            object propValue = prop.GetValue(message, null);
            _logger.Information("{Property} : {@Value}", prop.Name, propValue);
        }

        var response = await next(message, cancellationToken);

        //Response
        _logger.Information($"Handled {typeof(TResponse).Name}");
        return response;
    }
}