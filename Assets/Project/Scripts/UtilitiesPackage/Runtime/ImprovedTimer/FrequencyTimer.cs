using System;
using UnityEngine;
namespace UtilitiesLibrary.ImprovedTimers
{
    public class FrequencyTimer : Timer
    {
        public int TicksPerSecond { get; private set; }
        public event Action OnTick = delegate {};

        float timeThreshold = 0;
        public FrequencyTimer(int ticksPerSecond) : base(0)
        {
            CalculateTimeThreshold(ticksPerSecond);
        }


        public override bool IsFinished => !IsRunning;

        public override void Reset()
        {
            CurrentTime = 0;
        }

        public void Reset(int newTickPerSecond)
        {
            CalculateTimeThreshold(newTickPerSecond);
            Reset();
        }
        public override void Tick() {
            if (IsRunning && CurrentTime >= timeThreshold) {
                CurrentTime -= timeThreshold;
                OnTick.Invoke();
            }

            if (IsRunning && CurrentTime < timeThreshold) {
                CurrentTime += Time.deltaTime;
            }
        }
        private void CalculateTimeThreshold(int ticksPerSecond)
        {
            TicksPerSecond = ticksPerSecond;
            timeThreshold = 1f / ticksPerSecond;
        }
        
        
    }
}
