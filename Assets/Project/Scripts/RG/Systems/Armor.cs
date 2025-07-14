using System;
using UnityEngine;

namespace RG.Systems
{
    [Serializable]
    public class Armor : IRepairable
    {
        [SerializeField] private int _currentArmorHealth;
        public int CurrentArmorHealth => _currentArmorHealth;

        [SerializeField] private int _maxArmorHealth;
        public int MaxArmorHealth => _maxArmorHealth;

        public Action<int> OnAbsorbDamage;
        public Action OnBrokenArmor;

        public Armor(int maxArmorHealth)
        {
            _maxArmorHealth = maxArmorHealth;
            _currentArmorHealth = maxArmorHealth;
        }
        public void Repair(int amount) => Mathf.Clamp(_currentArmorHealth + amount, 0, MaxArmorHealth);

        /// <summary>
        /// Absorbs dealed damage into the armor and return left over damage in case armor was broken
        /// </summary>
        /// <param name="damageAbsorbed"></param>
        /// <returns></returns>
        public int AbsorbDamage(int damageAbsorbed)
        {
            if (damageAbsorbed == 0) return 0;
            int leftOverDamage = 0;
            if (_currentArmorHealth >= damageAbsorbed)
            {
                _currentArmorHealth -= damageAbsorbed;
            }
            else
            {
                leftOverDamage = damageAbsorbed - _currentArmorHealth;
                _currentArmorHealth = 0;
            }

            OnAbsorbDamage?.Invoke(damageAbsorbed);
            if (_currentArmorHealth <= 0) OnBrokenArmor?.Invoke();
            return leftOverDamage;
        }
    }
}