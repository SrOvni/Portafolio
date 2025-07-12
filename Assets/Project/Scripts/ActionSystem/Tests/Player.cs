using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;
using RG.Systems.Effects;
using Unity.VisualScripting;
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

        public EnergySystem ResistanceSystem => throw new System.NotImplementedException();

        public void Heal(int amount) => Health.Heal(amount);

        public void TakeDamage(int amount) => Health.TakeDamage(amount);

        // IReadOnlyList<IAction<ICharacter>> ladyBugPotionEffects = new EffectBuilder<ICharacter>()
            
        //     .Build();
    }
}