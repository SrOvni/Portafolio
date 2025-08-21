using System.Collections.Generic;

namespace UtilitiesLibrary.ImprovedTimers
{
    public static class TimerManager
    {
        static readonly List<Timer> timers = new List<Timer>();
        static private readonly object lockObject = new object();

        public static void RegisterTimer(Timer timer)
        {
            if (timer == null) return;
            lock (lockObject)
            {
                timers.Add(timer);
            } 
        }

        public static void DeregisterTimer(Timer timer)
        {
            if (timer == null) return;
            lock (lockObject)
            {
                timers.Remove(timer);
            }
        }

        public static void UpdateTimers()
        {
            lock (lockObject)
            {
                foreach (Timer timer in timers.ToArray())
                {
                    timer.Tick();
                }
            }
        }
        public static void Clear()
        {
            lock (lockObject)
            {
                timers.Clear();
            }
        }
    }
}