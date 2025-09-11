using UnityEngine;

public class CountdownTimer : Timer
{
    public CountdownTimer(float initialTime) : base(initialTime) { }

    public override bool IsFinished => CurrentTime <= 0;

    public override void Tick()
    {
        if (!IsRunning || CurrentTime <= 0) return;

        CurrentTime -= Time.deltaTime;

        if (CurrentTime <= 0)
        {
            CurrentTime = 0;
            Stop();
        }
    }
}

public class CountdownTimerToZero : Timer
{
    public CountdownTimerToZero(float initialTime) : base(initialTime) { }

    public override bool IsFinished => CurrentTime <= 0;

    public override void Tick()
    {
        if (!IsRunning || CurrentTime <= 0) return;

        CurrentTime = Mathf.Max(0, CurrentTime - Time.deltaTime);
        if (CurrentTime <= 0)
        {
            Stop();
        }
    }
}