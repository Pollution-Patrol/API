namespace PollutionPatrol.BuildingBlocks.Application.Interfaces;

public interface ICommand<out TResult> : IRequest<TResult>
{
}

public interface ICommand : IRequest
{
}

