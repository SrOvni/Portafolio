using System;
using System.Collections.Generic;

namespace RG.Systems
{
    public abstract class Potion<TTarget> : IEquatable<Potion<TTarget>>
    {
        public string Name { get;}
        public string Description { get;}
        public IList<IAction<TTarget>> Effects = new List<IAction<TTarget>>();
        public Potion(string name, string description, IList<IAction<TTarget>> effects)
        {
            Name = name;
            Description = description;
            Effects = effects;
        }

        public void Apply(TTarget target)
        {
            foreach (var effect in Effects)
            {
                effect?.Apply(target);
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Potion<TTarget> other)
        {
            return Name == other.Name && Effects == other.Effects; 
        }
    }
}