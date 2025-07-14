using UnityEngine;

namespace RG.Systems.Test
{
    class Player : MonoBehaviour, ICharacter
    {
        [SerializeField] Health _health;

        public int MaxHealth => _health.MaxHealth;

        public Health Health => _health;
        [SerializeField] private Armor _armor;

        public Armor Armor => _armor;
        [SerializeField] EnergySystem _energySystem;
        public EnergySystem ResistanceSystem => _energySystem;

        public void Heal(int amount) => Health.Heal(amount);

        public void TakeDamage(int amount) => Health.TakeDamage(amount);

        public int AbsorbDamage(int damage)
        {
            return Armor.AbsorbDamage(damage);
        }
    }
}