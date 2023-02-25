namespace PollutionPatrol.BuildingBlocks.Application.Pipelines;

public class ValidationPipelineBehaviour<TMessage, TResponse> :
    IPipelineBehavior<TMessage, TResponse> 
    where TMessage : IMessage
{
    private readonly IValidator<TMessage> _validator;

    public ValidationPipelineBehaviour(IValidator<TMessage> validator) => _validator = validator;

    public async ValueTask<TResponse> Handle(
        TMessage message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        var validationResult = await _validator.ValidateAsync(message, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            throw new InvalidMessageException(errors);
        }

        return await next(message, cancellationToken);
    }
}