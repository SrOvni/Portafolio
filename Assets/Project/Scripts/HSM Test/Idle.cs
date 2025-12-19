using RG.Systems.HSM;
using UnityEngine;

namespace Portafolio.HSM.Test
{
    public class Idle : State
    {
        private readonly PlayerContext _ctx;
        private readonly Grounded _parent;
        public Idle(StateMachine machine, State parent, PlayerContext ctx) : base(machine, parent)
        {
            _ctx = ctx;
            _parent = (Grounded)Parent;
        }
        public override State GetTransition()
        {
            return _ctx.Move != Vector3.zero ? _parent.Walk : null;
        }
        protected override void OnEnter()
        {
            Debug.Log("On Idle state");
        }
    }
}
