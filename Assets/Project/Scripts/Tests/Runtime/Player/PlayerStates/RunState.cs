using UnityEngine;

namespace RG.Systems.Tests.Player
{
    public class RunState : BaseState
    {
        private IPlayerControllerContext m_ctx;
        private readonly int RunHash;
        public RunState(IPlayerControllerContext ctx, Animator anim) : base(ctx, anim){
            m_ctx = ctx;
            RunHash = Animator.StringToHash("Run");
        }
        public override void OnEnter()
        {
            animator.CrossFade(RunHash, CROSS_FADE_DURATION);
            if (m_ctx == null) Debug.Log("Ctx null");
            if(m_ctx.MovementData == null) Debug.Log("Movement data null");
            m_ctx.CurrentSpeed = m_ctx.MovementData.RunSpeed;
        }
        public override void FixedUpdate()
        {
            m_ctx.HandleMovement();
        }
    }
}
