namespace RG.Systems
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }

}