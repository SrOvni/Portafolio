using UnityEngine;

namespace RG.Systems.Input
{
    public interface IInputReader
    {
        public Vector2 Direction { get; }
        public void EnablePlayerActions();
    }
}
