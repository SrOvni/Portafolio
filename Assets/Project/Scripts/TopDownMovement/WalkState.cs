using RG.Systems;
using UnityEngine;

namespace TopDownGame
{
    public class WalkState : BaseState
    {
        private readonly StateContext Ctx;
        private readonly string WALK_HASH = "Walk";
        public WalkState(StateContext ctx, Animator anim) : base(ctx, anim)
        {
            Ctx = ctx;
        }
        public override void OnEnter()
        {
            animator.CrossFade(WALK_HASH, CROSS_FADE_DURATION);
            Debug.Log("On Walk State");
        }
    }

}
