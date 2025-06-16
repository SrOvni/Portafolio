using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private UnityEvent OnReceiveDamage;
    [SerializeField] private UnityEvent<int> OnReceiveDamageInt;
    [SerializeField] private UnityEvent OnZeroHealth;
    [SerializeField] private int _initialHealth = 100;

    #region propeties
        public int CurrentHealth 
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }
    #endregion
    
    private void OnEnable()
    {
        _currentHealth = _initialHealth;
    }
    public void ReceiveDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        OnReceiveDamage?.Invoke();
        OnReceiveDamageInt?.Invoke(damageAmount);
        if (CurrentHealth <= 0)
        {
            OnZeroHealth?.Invoke();
        }
    }
    
}
