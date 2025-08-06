namespace RG.Systems
{
    public static class ActionUtils
    {
        public static void ApplyDamageType<TTarget, TDamage>(this TTarget target, TDamage damageType) where TTarget : struct, IAction<TDamage> where TDamage : struct
        {
            target.Apply(damageType);
        }
        
    }
}