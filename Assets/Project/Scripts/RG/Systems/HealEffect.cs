namespace RG.Systems
{
    public class HealEffect<TTarget> : Effect<TTarget> where TTarget : IHealable
    {
        private readonly int healValue;

        public HealEffect(int hv)
        {
            healValue = hv;
        }

        public override void Apply(TTarget target)
        {
            target.Heal(healValue);
        }
    }
}
