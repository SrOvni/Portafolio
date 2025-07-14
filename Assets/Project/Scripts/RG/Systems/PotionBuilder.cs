using System.Collections.Generic;
using Unity.VisualScripting;

namespace RG.Systems.Potion
{
    public class PotionBuilder<TTarget>
    {
        string _name;
        string _description;
        IList<IAction<TTarget>> _effects = new List<IAction<TTarget>>();

        public PotionBuilder<TTarget> SetName(string name)
        {
            _name = name;
            return this;
        }

        public PotionBuilder<TTarget> SetDescription()
        {
            _description = _description ?? "No description provided";
            return this;
        }

        public PotionBuilder<TTarget> AddEffect(IAction<TTarget> effect)
        {
            _effects.Add(effect);
            return this;
        }
        public PotionBuilder<TTarget> AddEffects(IList<IAction<TTarget>> effects)
        {
            _effects.AddRange(effects);
            return this;
        }
        public Potion<TTarget> Build()
        {

            return new CustomPotion(_name, _description, _effects);
        }
        public class CustomPotion : Potion<TTarget>
        {
            public CustomPotion(string name, string description, IList<IAction<TTarget>> effects) : base(name, description, effects){}
        }
    }
}

namespace RG.Systems.Test
{
    
}