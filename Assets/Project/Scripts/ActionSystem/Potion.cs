using System;
using System.Collections.Generic;

namespace RG.Systems.Potion
{
    public abstract class Potion<TTarget> : IEquatable<Potion<TTarget>>
    {
        public string Name { get;}
        public IList<IAction<ICharacter>> Effects = new List<IAction<ICharacter>>();
        public Potion(string name, string description, IList<IAction<TTarget>> effects)
        {
            Name = name;
        }

        public void Apply(ICharacter target)
        {
            foreach (var effect in Effects)
            {
                effect.Apply(target);
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