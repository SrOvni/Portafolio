using System;
public class AnimationDelegates
{
    private Action action;
    public AnimationDelegates onComplete(Action a)
    {
        action = a;
        return this;
    }
    public void OnComplete()
    {
        action?.Invoke();
    }
}
