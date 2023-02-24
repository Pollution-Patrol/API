namespace PollutionPatrol.BuildingBlocks.Application.Interfaces;

public interface IPagedQuery
{
    int Page { get; } 
    int Size { get; }
}