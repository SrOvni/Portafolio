using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    private Action _onComplete;
    private Action<float> _onUpdate;
    private float _duration; 
    private bool _isRunning;
    private float _elapsedTime;
    private TweenCallbacks _tweenCallbacks;
    private int _repetitions = 0;
    public TweenCallbacks TweenCallbacks { get { return _tweenCallbacks; } set { _tweenCallbacks = value; } }
    public Tween(float duration, Action<float> onUpdate)
    {
        _duration = duration;
        _onUpdate = onUpdate;
        _elapsedTime = 0f;
        _isRunning = true;
        TweenManager.Instance.RegisterTween(OnUpdate);
    }
    private void OnUpdate(float deltaTime)
    {
        if(_isRunning is false)return;

        _elapsedTime += deltaTime;
        float progress = Mathf.Clamp01(_elapsedTime/_duration);

        _onUpdate?.Invoke(progress);
        
        if(progress >= 1f)
        {
            Complete();
        }


    }

    private void Complete()
    {
        if(_repetitions > 0)
        {
            _elapsedTime = 0;
            --_repetitions;
            return;
        }
        _isRunning = false;
        _onComplete?.Invoke();
        TweenManager.Instance.UnregisterActions(OnUpdate);

    }
    public void Kill()
    {
        _isRunning = false;
        TweenManager.Instance.UnregisterActions(OnUpdate);
    }
    public Tween onComplete(Action action)
    {
        _onComplete = action;
        return this;
    }
    private void OnDestroy() {
        Kill();
    }
    public Tween Repeat(int repetitions)
    {
        _repetitions = repetitions;
        return this;
    }
}
