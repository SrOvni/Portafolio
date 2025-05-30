# Utilities Library
## Time Manager
An static class for time implementations
### Timer
A base clase for:
- Countdown Timer
### ¿How to start a countdown timer?
```c#
public void Start()
{
    timer = new CountDownTimer(timeDuration);
    timer.OnTimeStart += () Debug.Log("Timer started");
    timer.OnTimeStop += () Debug.Log("Timer stoped");
    timer.Start();
}
```
### How to dispose a timer safely
```c#
public void OnDestroy()
{
    timer.Dispose();
}
```
## State Machine
A quick start when creating a state machine with a simple base state machine logic
### State Manager
The brain of the state machine and the context given to the concrete states
### Base State
An abstract class with Start, Update and Exit State methods.
