using RG.Systems.HSM;

namespace Portafolio.HSM.Test
{
    public class Grounded : State
    {
        readonly PlayerContext _ctx;
        public Idle Idle;
        public Walk Walk;
        public Grounded(StateMachine machine, State parent, PlayerContext ctx) : base(machine, parent)
        {
            _ctx = ctx;
            Idle = new(machine, this, ctx);
            Walk = new(machine, this, ctx);
        }

        public override State GetInitialState() => Idle;
        public override State GetTransition()
        {
            if (_ctx.Attacking)
            {
                //Verifications
                return ((PlayerRoot)Parent).Attacking;
            }
            else
            {
                return null;
            }
        }
    }
}
