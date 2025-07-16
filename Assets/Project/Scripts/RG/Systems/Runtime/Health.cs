using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

namespace RG.Systems
{
    [Serializable]
    public class Health
    {
        [SerializeField] private int _currentHealth;
        public int CurrentHealth => _currentHealth;
        [SerializeField] public int MaxHealth => _maxHealth;
        [SerializeField] private int _maxHealth;
        public event Action<int> OnTakeDamage;
        public event Action OnZeroHealth;
        public event Action<int> OnHealed;

        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth = Mathf.Max(_currentHealth - amount, 0);
            OnTakeDamage.Invoke(amount);
            
            if (_currentHealth <= 0) OnZeroHealth.Invoke();
        }
        public void Heal(int amount)
        {
            if (amount < 1 || _currentHealth == MaxHealth) return;
            int previous = _currentHealth;

            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, MaxHealth);
            int healedAmount = CurrentHealth - previous;
            OnHealed?.Invoke(healedAmount);
        }
    }
    
}