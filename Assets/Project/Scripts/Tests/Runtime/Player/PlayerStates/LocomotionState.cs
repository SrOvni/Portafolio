using System;
using UnityEngine;

namespace RG.Systems.Tests.Player
{
    public class LocomotionState : BaseState
    {
        readonly int LocomotionHash;
        const float crossFadeDuration = 0.1f;

        readonly IPlayerControllerContext m_ctx;
        public LocomotionState(IPlayerControllerContext context, Animator animator) : base(context, animator)
        {
            m_ctx = context;
            LocomotionHash = Animator.StringToHash("Locomotion");
        }

        public override void OnEnter()
        {
            m_ctx.CurrentSpeed = m_ctx.MovementData.WalkSpeed;
            animator.CrossFade(LocomotionHash, crossFadeDuration);
        }
        public override void FixedUpdate()
        {
            m_ctx.HandleMovement();
        }
    }

    public class ExampleState : BaseState
    {
        public ExampleState(IStateContext ctx, Animator anim) : base(ctx, anim)
        {

        }
    }
}
