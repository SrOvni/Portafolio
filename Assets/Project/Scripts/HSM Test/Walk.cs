using RG.Systems.HSM;
using UnityEngine;

namespace Portafolio.HSM.Test
{
    public class Walk : State
    {
        private readonly PlayerContext _ctx;
        private readonly Grounded _parent;
        private readonly PlayerRoot root;

        public Walk(StateMachine machine, State parent, PlayerContext ctx) : base(machine, parent)
        {
            _ctx = ctx;
            _parent = (Grounded)Parent;
            root = (PlayerRoot)Parent;
        }
        public override State GetTransition()
        {
            if(_ctx.Attacking)return root.Attacking;
            return _ctx.Move == Vector3.zero ? _parent.Idle : null;
        }
        protected override void OnEnter()
        {
            Debug.Log("On walk state");
        }
        protected override void OnUpdate(float deltaTime)
        {
            // _ctx.CharacterController.Move(_ctx.Move.normalized *  
        }
    }
}
