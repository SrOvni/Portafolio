using System;
using UnityEngine;
using static UnityEngine.Mathf;

namespace RG.Systems
{
    [Serializable]
    public class Armor : IArmor
    {
        [SerializeField, Min(0)] private int _currentArmor;
        [SerializeField, Min(0)] private int _maxArmor;

        public int CurrentArmor => _currentArmor;

        public int MaxArmor => _maxArmor;

        public event Action<int> OnArmorRepaired;
        public event Action<int> OnArmorDamaged;

        /// <summary>
        /// Repairs the armor by the specified amount
        /// </summary>
        /// <param name="amount">Amount to repair</param>
        public void Repair(int amount)
        {
            if (amount <= 0) return;
            int repairedAmount = SetArmor(_currentArmor + amount);
            OnArmorRepaired?.Invoke(repairedAmount);
        }

    
        /// <summary>
        /// Take the damage to the specified amount.
        /// </summary>
        /// <param name="amount">Amount to damage</param>
        public void TakeDamage(int amount)
        {
            if (amount <= 0) return;
            int damagedAmount = SetArmor(_currentArmor - amount);
            OnArmorDamaged?.Invoke(damagedAmount);
        }
        int SetArmor(int target)
        {
            int clamped = Clamp(target, 0, MaxArmor);
            int delta = Abs(_currentArmor - clamped);
            _currentArmor = clamped;
            return delta;
        }
    }

    public interface IArmor : IDamagable, IRepairable
    {
        public int CurrentArmor { get; }
        public int MaxArmor { get; }

    }
}