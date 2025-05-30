using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;
using UtilitiesLibrary.ImprovedTimers;
using UtilitiesLibrary.LowLevel;

namespace UtilitiesLibrary
{
    internal static class TimerBootstrapper
    {
        static PlayerLoopSystem timerSystem;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize()
        {
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (!InsertTimeManager<Update>(ref currentPlayerLoop, 0))
            {
                Debug.Log("Improved Timers not initialized unable to register TimeManager into the update loop");
                return;
            }
            PlayerLoop.SetPlayerLoop(currentPlayerLoop);
            PlayerLoopUtils.PrintPlayerLoop(currentPlayerLoop);

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayerModeState;
            EditorApplication.playModeStateChanged += OnPlayerModeState;
#endif
        }

        private static void OnPlayerModeState(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                PlayerLoopSystem curretPlayerMode = PlayerLoop.GetCurrentPlayerLoop();
                RemoveTimeManager<Update>(ref curretPlayerMode);
                PlayerLoop.SetPlayerLoop(curretPlayerMode);

                TimerManager.Clear();
            }
        }

        static void RemoveTimeManager<T>(ref PlayerLoopSystem loop)
        {
            PlayerLoopUtils.RemoveSystem<T>(ref loop, in timerSystem);
        }

        static bool InsertTimeManager<T>(ref PlayerLoopSystem loop, int index)
        {
            timerSystem = new PlayerLoopSystem()
            {
                type = typeof(TimerManager),
                updateDelegate = TimerManager.UpdateTimers,
                subSystemList = null
            };

            return PlayerLoopUtils.InsertSystem<T>(ref loop, in timerSystem, index);
        }
    }
}
