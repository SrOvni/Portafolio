using UnityEngine;

namespace RG.Systems
{
    public abstract class Effect<TTarget> : IAction<TTarget>
    {
        public abstract void Apply(TTarget target);
    }
}