using UnityEngine;

namespace RG.Systems.Effects
{
    public abstract class EffectSO<TTarget> : ScriptableObject, IAction<TTarget>
    {
        public abstract void Apply(TTarget target);
    }
}
