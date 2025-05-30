using System.Collections.Generic;

namespace UtilitiesLibrary.ImprovedTimers
{
    public static class TimerManager
    {
        static readonly List<Timer> timers = new List<Timer>();

        public static void RegisterTimer(Timer timer)
        {
            if (timer == null) return;
            timers.Add(timer);
        }

        public static void DeregisterTimer(Timer timer)
        {
            if (timers == null) return;
            timers.Add(timer);
        }

        public static void UpdateTimers()
        {
            foreach (Timer timer in timers)
            {
                timer.Tick();
            }
        }
        public static void Clear()
        {
            timers.Clear();
        }
    }
}