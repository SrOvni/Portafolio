using System;

namespace RG.Systems
{
    public class FuncPredicate : IPredicate
    {
        Func<bool> _func;
        public FuncPredicate(Func<bool> func)
        {
            _func = func;
        }
        public bool Evaluate()
        {
            return _func.Invoke();
        }
    }
}
