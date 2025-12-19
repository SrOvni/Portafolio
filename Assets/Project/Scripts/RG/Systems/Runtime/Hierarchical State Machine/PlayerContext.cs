using System;
using UnityEngine;

namespace RG.Systems.HSM
{
    [Serializable]
    public class PlayerContext
    {
        public Vector3 Move;
        public Vector3 Velocity;
        // public Vector3 MovementDirection;
        public bool Grounded;
        public float MoveSpeed = 6f;
        public float Accel = 40f;
        public float JumpSpeed = 7f;
        public bool JumpPressed;
        public Animator Anim;
        public bool Attacking;
        public CharacterController CharacterController;
    }
}
