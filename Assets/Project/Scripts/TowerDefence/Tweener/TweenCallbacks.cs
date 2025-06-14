using System;
using UnityEngine;

public class TweenCallbacks : MonoBehaviour
{
    private Action action;
    public TweenCallbacks onComplete(Action a)
    {
        action = a;
        return this;
    }
    public void OnComplete()
    {
        action?.Invoke();
    }
}
