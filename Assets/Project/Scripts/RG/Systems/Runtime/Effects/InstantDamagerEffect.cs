namespace RG.Systems.Effects
{
        public class InstantDamagerEffect : IAction<IDamageable>
    {
        readonly int _damageValue;

        public InstantDamagerEffect(int damageAmount)
        {
            _damageValue = damageAmount;
        }
        public void Apply(IDamageable target)
        {
            target.TakeDamage(_damageValue);
        }
    }
}
