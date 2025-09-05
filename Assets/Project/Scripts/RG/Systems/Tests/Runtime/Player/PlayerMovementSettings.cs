using UnityEngine;

namespace RG.Systems.Tests.Player
{
    [CreateAssetMenu(fileName = "Movement Settings", menuName = "RG.Tests/Movemenent Settings")]
    public class PlayerMovementSettings : ScriptableObject
    {
        [Header("Walk")]
        public float WalkSpeed = 5;
        [Header("Run")]
        public float RunSpeed = 5;
        [Header("Jump")]
        public float JumpForce = 10;
    }
}
