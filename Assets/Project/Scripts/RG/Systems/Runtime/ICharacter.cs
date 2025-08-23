namespace RG.Systems
{
    public interface ICharacter : IHealable, IDamageable
    {
        public Health Health { get; }
        public Armor Armor { get; }

        public EnergySystem EnergySystem { get; }
    }
}