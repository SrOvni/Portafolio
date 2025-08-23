namespace RG.Systems
{
    public readonly struct DamageAction<TDamage> : IAction<TDamage> where TDamage : struct, IDamage
    {
        readonly IDamageable _target;
        public DamageAction(IDamageable targetToDamage)
        {
            _target = targetToDamage;
        }
        
        public void Apply(TDamage damageType)
        {
            damageType.MakeDamage(_target);
        }
    }

}