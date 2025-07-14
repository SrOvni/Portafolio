using UnityEngine;

namespace RG.Systems.Effects
{
    [CreateAssetMenu(fileName = "IntantDamageEffect", menuName = "Effects/Instant Damage Effect", order = 0)]
    public class IntantDamageEffectSO : EffectSO<IDamagable>
    {
        [SerializeField] private int _instantDamageAmount;
        public int InstantDamageAmount => _instantDamageAmount;
        public override void Apply(IDamagable target)
        {
            target.TakeDamage(InstantDamageAmount);
        }
    }
}
