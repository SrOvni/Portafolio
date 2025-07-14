using UnityEngine;
using RG.Systems.Effects;
using System.Collections.Generic;

namespace RG.Systems.Test
{
    class Player : MonoBehaviour, ICharacter
    {
        [SerializeField] Health _health;

        public int MaxHealth => _health.MaxHealth;

        public Health Health => _health;
        [SerializeField] private Armor _armor;

        public Armor Armor => _armor;

        List<Effect<ICharacter>> _effects;


        Potion<ICharacter> weirdPotion = new PotionBuilder<ICharacter>()
            .SetName("Poción de prueba")
            .SetDescription("Es una poción de prueba")
            .AddEffect(new InstantHealEffect(100))
            .AddEffect(new InstantDamagerEffect(100))
            .Build();

        public EnergySystem ResistanceSystem => throw new System.NotImplementedException();

        public void Heal(int amount) => Health.Heal(amount);

        public void TakeDamage(int amount) => Health.TakeDamage(amount);

        public void ApplyWeirdPotion()
        {
            weirdPotion.Apply(this);
        }
    }    

}