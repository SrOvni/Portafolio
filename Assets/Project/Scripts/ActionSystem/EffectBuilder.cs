using System.Collections.Generic;

namespace RG.Systems.Effects
{
    public class EffectBuilder<TTarget> where TTarget : ICharacter
    {
        IList<IAction<TTarget>> effects = new List<IAction<TTarget>>();
        public EffectBuilder()
        {

        }
        public IList<IAction<TTarget>> Build()
        {
            return effects;
        }

        public EffectBuilder<TTarget> AddEffect(IAction<TTarget> effect)
        {
            effects.Add(effect);
            return this;
        }
        public EffectBuilder<TTarget> AddHealEffect(int healValue)
        {
            effects.Add(new HealEffect<TTarget>(healValue));
            return this;
        }

    }
    public class HealEffect<TTarget> : IAction< TTarget> where TTarget : IHealable
    {
        private int healValue;

        public HealEffect(int hv)
        {
            healValue = hv;
        }

        public void Apply(TTarget target)
        {
            target.Heal(healValue);
        }
    }

    public class DamagerEffect<TTarget> : IAction<TTarget> where TTarget : IDamagable
    {
        int damageValue;
        public void Apply(TTarget target)
        {
            target.TakeDamage(damageValue);
        }
    }
}
namespace RG.Systems.Potion.Test
{
    public class LadyBugPotion : IAction<ICharacter>
    {
        private int _healAmount;
        private int _resistanceAmount;

        public LadyBugPotion(int healAmount, int resistanceAmount)
        {
            _healAmount = healAmount;
            _resistanceAmount = resistanceAmount;
        }
        public void Apply(ICharacter target)
        {
            target.Heal(_healAmount);
        }
    }

    public class PotionBrewingStand
    {
        // PotionBuilder potionBuilder;
    }
}
