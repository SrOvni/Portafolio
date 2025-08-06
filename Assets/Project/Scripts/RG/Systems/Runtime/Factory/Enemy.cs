using UnityEngine;
namespace RG.Systems.Test
{
    public abstract class Enemy : IDamagable, IHealable
    {

        public Health Health { get; }
        public Armor Armor { get; }
        IAction<IDamage> _damageAction;

        
        public void Heal(int amount)
        {
            Health.Heal(amount);
        }

        public void TakeDamage(int amount)
        {
            Health.TakeDamage(amount);
        }
        public virtual void MakeDamage(IDamagable target)
        {
            var damageType = new MeleeDamage(10);
            var targetToDamage = new DamageAction<MeleeDamage>(target);
            
            targetToDamage.ApplyDamageType(damageType);
        }
    }

}