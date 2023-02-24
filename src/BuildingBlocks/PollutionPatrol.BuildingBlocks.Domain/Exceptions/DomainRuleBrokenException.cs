namespace PollutionPatrol.BuildingBlocks.Domain.Exceptions;

public class DomainRuleBrokenException : Exception
{
    public DomainRuleBrokenException(IDomainRule brokenRule) : base(brokenRule.Message)
    {
        BrokenRule = brokenRule;
        Details = brokenRule.Message;
    }
    
    public IDomainRule BrokenRule { get; }

    public string Details { get; }

    public override string ToString() => $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
}