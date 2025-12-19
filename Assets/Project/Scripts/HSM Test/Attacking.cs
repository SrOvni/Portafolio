using RG.Systems.HSM;

namespace Portafolio.HSM.Test
{
    public class Attacking : State
    {
        readonly PlayerContext _ctx;
        public Attacking(StateMachine machine, State parent, PlayerContext ctx) : base(machine, parent)
        {
            _ctx = ctx;
        }
        public override State GetTransition() => _ctx.Attacking ? null : ((PlayerRoot)Parent).Grounded;
    }
}
