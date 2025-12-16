using LitJson;
using UnityEngine;
using static RG.Systems.SavingService;

namespace RG.Systems.Tests.Player
{
    [CreateAssetMenu(fileName = "Movement Data", menuName = "RG.Tests/Movemenent Data")]
    public class PlayerMovementData : ScriptableObject
    {
        [Header("Walk")]
        public float WalkSpeed = 5;
        [Header("Run")]
        public float RunSpeed = 5;
        [Header("Jump")]
        public float JumpForce = 10;
    }
}
