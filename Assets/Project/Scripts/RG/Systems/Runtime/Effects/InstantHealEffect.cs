namespace RG.Systems.Effects
{
        public class InstantHealEffect : IAction<IHealable>
    {
        readonly int healValue;

        public InstantHealEffect(int hv)
        {
            healValue = hv;
        }

        public void Apply(IHealable target)
        {
            target.Heal(healValue);
        }
    }
}
