using RG.Systems.HSM;

namespace Portafolio.HSM.Test
{
    public class PlayerRoot : State
    {
        public readonly Grounded Grounded;
        public readonly Attacking Attacking;
        readonly PlayerContext _ctx;
        public PlayerRoot(StateMachine machine, PlayerContext ctx) : base(machine)
        {
            _ctx = ctx;
            Grounded = new(machine, this, ctx);
            Attacking = new(machine, this, ctx);
        }

        public override State GetInitialState() => Grounded;
        public override State GetTransition() => _ctx.Attacking ? Attacking : null;
    }
}
