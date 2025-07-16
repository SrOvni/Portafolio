using System;
using System.Threading;
using UnityEngine;
//To do agregar Timers
namespace RG.Systems
{
    [Serializable]
    public class EnergySystem
    {
        [SerializeField, Min(0)] private int _currentEnergy;
        [SerializeField, Min(0)] private int _maxEnergy;

        public int CurrentEnergy { get => _currentEnergy; }
        public int MaxEnergy { get => _maxEnergy; }

        public event Action<int> OnEnergyConsume;
        public event Action<int> OnRecoveryConsume;

        //To-do Agregar consume y recovery timers
        // private Timer _consumeTimer = new CountDownTimer();
        private Timer _recoveryTimer;

        public EnergySystem(int maxEnergy)
        {
            _maxEnergy = maxEnergy;
        }


        public void ConsumeEnergy()
        {

        }
        public void RecoverEnergy()
        {

        }
    }
}