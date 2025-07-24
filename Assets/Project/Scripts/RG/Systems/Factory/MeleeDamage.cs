namespace RG.Systems.Test
{
    public readonly struct MeleeDamage : IDamage
    {
        private readonly int _amount;
        public MeleeDamage(int amount)
        {
            _amount = amount;
        }


        public void MakeDamage(IDamagable receiver)
        {
            receiver.TakeDamage(_amount);
        }
    }

}