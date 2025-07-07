using System;
namespace FiniteStateMachine
{
    public class FuncPredicate : IPredicate
    {
        readonly Func<bool> _func;
        FuncPredicate(Func<bool> func)
        {
            _func = func;
        }
        public bool Evaluate() => _func.Invoke();
    }
}