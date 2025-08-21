using System.Collections.Generic;
using UnityEngine;

namespace UtilitiesLibrary
{
    public static class Logger
    {
        static HashSet<string> loggedMessages = new HashSet<string>();
        public static void Log(string Message, LogType logType, Object source)
        {
            switch (logType)
            {
                case LogType.Log:
                    if (loggedMessages.Contains(Message)) return;
                    Debug.Log(Message, source);
                    loggedMessages.Add(Message);
                    break;
                case LogType.Warning:
                    if (loggedMessages.Contains(Message)) return;
                    Debug.LogWarning(Message, source);
                    loggedMessages.Add(Message);
                    break;
                case LogType.Error:
                    if (loggedMessages.Contains(Message)) return;
                    Debug.LogError(Message, source);
                    loggedMessages.Add(Message);
                    break;
            }
        }
    }
}
