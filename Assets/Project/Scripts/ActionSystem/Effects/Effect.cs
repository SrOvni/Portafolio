using UnityEngine;

namespace RG.Systems.Effects
{
    public abstract class Effect<TTarget> : IAction<TTarget>
    {
        public abstract void Apply(TTarget target);
    }
}