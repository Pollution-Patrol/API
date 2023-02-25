namespace PollutionPatrol.BuildingBlocks.Application.Pipelines;

public class MessageValidationBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public MessageValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => 
        _validators = validators;

    public async ValueTask<TResponse> Handle(
        TRequest message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(message);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .Select(x => x.ErrorMessage)
                .ToList();

            if (errors.Count != 0)
                throw new InvalidMessageException(errors);
        }

        return await next(message, cancellationToken);
    }
}