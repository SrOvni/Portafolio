using RG.Systems;
using UnityEngine;

namespace TopDownGame
{
    public abstract class HierarchicalStates : BaseState
    {
        public abstract bool IsRootState { get;}
        protected HierarchicalStates(StateContext ctx, Animator anim) : base(ctx, anim)
        {
            
        }
    }

}
