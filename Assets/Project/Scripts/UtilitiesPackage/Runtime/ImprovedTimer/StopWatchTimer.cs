using UnityEngine;
namespace UtilitiesLibrary.ImprovedTimers
{
    /// <summary>
    /// Measures the lenght of an interval of time
    /// </summary>
    public class StopWatchTimer : Timer
    {
        public StopWatchTimer() : base(0) { }

        public override bool IsFinished => false;

        public override void Tick()
        {
            if (IsRunning)
            {
                CurrentTime += Time.deltaTime;
            }
        }
    }
}

