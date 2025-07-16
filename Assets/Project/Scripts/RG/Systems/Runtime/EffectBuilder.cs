using System.Collections.Generic;

namespace RG.Systems
{
    public class EffectBuilder<TTarget>
    {
        private readonly IList<IAction<TTarget>> _effects = new List<IAction<TTarget>>();

        public EffectBuilder()
        {
            
        }

        public EffectBuilder<TTarget> AddEffect(IAction<TTarget> effect)
        {
            _effects?.Add(effect);
            return this;
        }

        public IList<IAction<TTarget>> Build()
        {
            return _effects;
        }

    }
}
