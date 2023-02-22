namespace PollutionPatrol.BuildingBlocks.Infrastructure.DomainEventsDispatching;

internal sealed class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IDomainEventsAccessor _domainEventsAccessor;
    private readonly IMediator _mediator;

    public DomainEventsDispatcher(IDomainEventsAccessor domainEventsAccessor, IMediator mediator)
    {
        _domainEventsAccessor = domainEventsAccessor;
        _mediator = mediator;
    }

    public async Task DispatchEventsAsync(DbContext dbContext)
    {
        var domainEvents = _domainEventsAccessor.GetAllDomainEvents(dbContext);
        
        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent);
        
        _domainEventsAccessor.ClearAllDomainEvents(dbContext);
    }
}