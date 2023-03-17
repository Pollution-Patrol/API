namespace PollutionPatrol.Modules.Pollution.Infrastructure;

internal sealed class PollutionModule : IPollutionModule
{
    private readonly IMediator _mediator;

    public PollutionModule(IMediator mediator) => _mediator = mediator;

    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command) => await _mediator.Send(command);

    public async Task ExecuteCommandAsync(ICommand command) => await _mediator.Send(command);

    public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query) => await _mediator.Send(query);
}