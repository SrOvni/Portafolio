using System;
using UnityEngine;
using UtilitiesLibrary.ImprovedTimers;

public abstract class Timer : IDisposable
{
    public float CurrentTime { get; protected set; }
    public bool IsRunning { get; private set; }

    protected float _initialTime;

    public float Progress => Mathf.Clamp(CurrentTime / _initialTime, 0, 1);

    public Action OnTimeStart = delegate { };
    public Action OnTimeStop = delegate { };



    public Timer(float initialTime)
    {
        _initialTime = initialTime;
    }

    public void Start()
    {
        CurrentTime = _initialTime;
        if (!IsRunning)
        {
            IsRunning = true;
            TimerManager.RegisterTimer(this);
            OnTimeStart?.Invoke();
        }
    }

    public void Stop()
    {
        if (IsRunning)
        {
            IsRunning = false;
            TimerManager.DeregisterTimer(this);
            OnTimeStop?.Invoke();
        }
    }

    public abstract void Tick();
    public abstract bool IsFinished { get; }

    public void Resume() => IsRunning = true;
    public void Pause() => IsRunning = false;

    public virtual void Reset() => CurrentTime = _initialTime;
    public virtual void Reset(float newTime)
    {
        _initialTime = newTime;
        Reset();

    }
    bool disposed;
    //Call dispose to ensure deregistration of the timer from the TimeManager
    //when the consumer is donde with the timer or being destroyed
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    ~Timer()
    {
        Dispose(false);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;

        if (disposing)
        {
            TimerManager.DeregisterTimer(this);
        }
        disposed = true;
    }
}