using System;
using UnityEngine;

namespace RG.Systems.Tests.Player
{
    public class LocomotionState : BaseState
    {
        readonly int LocomotionHash;
        const float crossFadeDuration = 0.1f;

        IPlayerControllerContext _ctx;
        public LocomotionState(IPlayerControllerContext context, Animator animator) : base(context, animator)
        {
            _ctx = context;
            LocomotionHash = Animator.StringToHash("Locomotion");
        }

        public override void OnEnter()
        {
            Debug.Log("LocomotionState");
            // if (animator is null) Debug.Log("Animtor is null");
            // if (animator == null) Debug.Log("Animator is null");
            animator.CrossFade(LocomotionHash, crossFadeDuration);
        }
        public override void FixedUpdate()
        {
            // Debug.Log("Handling movement");
            _ctx.HandleMovement();
        }
    }

    public class ExampleState : BaseState
    {
        public ExampleState(IStateContext ctx, Animator anim) : base(ctx, anim)
        {

        }
    }
}
