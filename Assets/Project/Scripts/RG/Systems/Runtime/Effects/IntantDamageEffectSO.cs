using UnityEngine;

namespace RG.Systems.Effects
{
    [CreateAssetMenu(fileName = "IntantDamageEffect", menuName = "Effects/Instant Damage Effect", order = 0)]
    public class IntantDamageEffectSO : EffectSO<IDamageable>
    {
        [SerializeField] private int _instantDamageAmount;
        public int InstantDamageAmount => _instantDamageAmount;
        public override void Apply(IDamageable target)
        {
            target.TakeDamage(InstantDamageAmount);
        }
    }
}
