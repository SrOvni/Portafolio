using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using log4net.Core;
using UnityEngine;
using UnityEngine.LowLevel;

namespace UtilitiesLibrary.LowLevel
{
    public static class PlayerLoopUtils
    {
        //Remove a system from the player loop
        public static void RemoveSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToRemove)
        {
            if (loop.type == typeof(T)) return;

            var playerLoopSystemList = new List<PlayerLoopSystem>(loop.subSystemList);

            for (int i = 0; i < playerLoopSystemList.Count; i++)
            {
                if (playerLoopSystemList[i].type == systemToRemove.type && playerLoopSystemList[i].updateDelegate == systemToRemove.updateDelegate)
                {
                    playerLoopSystemList.RemoveAt(i);
                    loop.subSystemList = playerLoopSystemList.ToArray();
                }
            }

            HandlerSubSystemLoopRemoval<T>(ref loop, systemToRemove);

        }

        public static void HandlerSubSystemLoopRemoval<T>(ref PlayerLoopSystem loop, PlayerLoopSystem systemToRemove)
        {
            if (loop.subSystemList == null) return;
            for (int i = 0; i < loop.subSystemList.Length; ++i)
            {
                RemoveSystem<T>(ref loop.subSystemList[i], systemToRemove);
            }
        }
        //Insert a system into de player loop
        public static bool InsertSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.type != typeof(T)) return HandleSubSytemLoop<T>(ref loop, systemToInsert, index);

            var playerLoopSystemList = new List<PlayerLoopSystem>();
            if (loop.subSystemList != null) playerLoopSystemList.AddRange(loop.subSystemList);
            playerLoopSystemList.Insert(index, systemToInsert);
            loop.subSystemList = playerLoopSystemList.ToArray();
            return true;


        }

        private static bool HandleSubSytemLoop<T>(ref PlayerLoopSystem loop, PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.subSystemList == null) return false;

            for (int i = 0; i < loop.subSystemList.Length; i++)
            {
                if (!InsertSystem<T>(ref loop.subSystemList[i], systemToInsert, index)) continue;
                return true;
            }
            return false;
        }

        public static void PrintPlayerLoop(PlayerLoopSystem loop)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Unity Player Loop");
            foreach (PlayerLoopSystem subSystem in loop.subSystemList)
            {
                PrintSubsytem(subSystem, sb, 0);
            }
            Debug.Log(sb.ToString());
        }

        static void PrintSubsytem(PlayerLoopSystem system, StringBuilder sb, int level)
        {
            sb.Append(' ', level * 2).AppendLine(system.type.ToString());
            if (system.subSystemList == null || system.subSystemList.Length == 0) return;
            
            foreach (PlayerLoopSystem subsystem in system.subSystemList)
            {
                PrintSubsytem(subsystem, sb, level + 1);
            }
        }
    }
}