using UnityEngine;

namespace TopDownGame
{
    public class GroundState : HierarchicalStates
    {
        private bool _isRootState;

        public GroundState(StateContext ctx, Animator anim) : base(ctx, anim)
        {
            _isRootState = true;
        }

        public override bool IsRootState => _isRootState;
    }

}
