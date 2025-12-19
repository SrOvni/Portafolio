using UnityEngine;

namespace TopDownGame
{
    public class IdleState : HierarchicalStates
    {
        public override bool IsRootState => _isRootState;
        private readonly StateContext Ctx;
        private readonly string IDLE_HASH = "Idle";
        private bool _isRootState;

        public IdleState(StateContext ctx, Animator anim) : base(ctx, anim)
        {
            Ctx = ctx;
            _isRootState = false;
        }
        public override void OnEnter()
        {
            animator.CrossFade(IDLE_HASH, CROSS_FADE_DURATION);
            Debug.Log("On idle state");
        }
    }

}
