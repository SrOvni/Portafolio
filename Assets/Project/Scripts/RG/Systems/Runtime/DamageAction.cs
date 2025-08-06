namespace RG.Systems
{
    public readonly struct DamageAction<TDamage> : IAction<TDamage> where TDamage : struct, IDamage
    {
        readonly IDamagable _target;
        public DamageAction(IDamagable targetToDamage)
        {
            _target = targetToDamage;
        }
        
        public void Apply(TDamage damageType)
        {
            damageType.MakeDamage(_target);
        }
    }

}