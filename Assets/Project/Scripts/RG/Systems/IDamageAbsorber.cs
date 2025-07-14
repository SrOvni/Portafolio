namespace RG.Systems
{
    public interface IDamageAbsorber : IDamagable
    {
        public int AbsorbDamage(int damage);
    }
}