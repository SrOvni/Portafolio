using UnityEngine;

namespace RG.Systems.Tests.Player
{
    public interface IPlayerControllerContext : IStateContext
    {
        GameObject player { get; }
        public float CurrentSpeed { get; set; }
        PlayerMovementData MovementData { get; }
        void HandleMovement();
    }

}