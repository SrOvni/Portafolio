namespace FiniteStateMachine
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Predicate { get; }

    }
}
