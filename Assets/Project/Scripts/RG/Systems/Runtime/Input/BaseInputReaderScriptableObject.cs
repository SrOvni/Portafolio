using System;
using UnityEngine;
using UnityEngine.Events;

namespace RG.Systems.Input
{

    public abstract class BaseInputReaderScriptableObject : ScriptableObject, IInputReader
    {
        public abstract Vector2 Direction { get; }
        public abstract void EnablePlayerActions();
    }
}
