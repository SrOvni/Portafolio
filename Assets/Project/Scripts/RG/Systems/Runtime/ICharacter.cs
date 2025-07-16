namespace RG.Systems
{
    public interface ICharacter : IHealable, IDamagable
    {
        public Health Health { get; }
        public Armor Armor { get; }

        public EnergySystem EnergySystem { get; }
    }
}