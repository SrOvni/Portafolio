namespace RG.Systems.Effects
{
        public class InstantDamagerEffect : IAction<IDamagable>
    {
        readonly int _damageValue;

        public InstantDamagerEffect(int damageAmount)
        {
            _damageValue = damageAmount;
        }
        public void Apply(IDamagable target)
        {
            target.TakeDamage(_damageValue);
        }
    }
}
