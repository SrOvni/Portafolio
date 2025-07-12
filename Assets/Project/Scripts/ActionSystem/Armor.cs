using System;
using UnityEngine;

namespace RG.Systems
{
    [Serializable]
    public class Armor : IRepairable, IDamagable
    {
        [SerializeField] Health health;
        public void Repair(int amount) => health.Heal(amount);

        public void TakeDamage(int amount) => health.TakeDamage(amount);
    }
}