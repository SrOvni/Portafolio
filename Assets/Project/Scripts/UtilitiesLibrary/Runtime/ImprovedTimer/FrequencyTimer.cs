using System;
using UnityEngine;
public class FrequencyTimer : Timer
{
    public override bool IsFinished => !IsRunning;
    public event Action OnTick = delegate { };
    public float TickPerSeconds { get; private set; }
    float timeThreshold;
    public float nextTickTime;
    public FrequencyTimer(float tickPerSecond) : base(0)
    {
        CalculateTimeThreshold(tickPerSecond);
    }

    private void CalculateTimeThreshold(float tickPerSecond)
    {
        TickPerSeconds = tickPerSecond;
        timeThreshold = 1f / tickPerSecond;
    }

    public override void Tick()
    {
        if (IsRunning && CurrentTime >= timeThreshold)
        {
            CurrentTime -= timeThreshold;
            OnTick?.Invoke();
        }
        if (IsRunning && CurrentTime <= timeThreshold)
        {
            CurrentTime += Time.deltaTime;
        }
    }
    public override void Reset()
    {
        CurrentTime = 0;
    }
    public void Reset(int newTicksPerSecond)
    {
        CalculateTimeThreshold(newTicksPerSecond);
        Reset();
    }
}
