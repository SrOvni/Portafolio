using UnityEngine;

namespace RG.Systems.Effects
{
    [CreateAssetMenu(fileName = "InstantHealEffect", menuName = "Effects/Instant Heal Effect")]
    public class IntantHealEffectSO : ScriptableObject, IAction<IHealable>
    {
        [SerializeField] private int instantHealAmount;

        public int InstantHealAmount => instantHealAmount;

        public void Apply(IHealable target)
        {
            target.Heal(InstantHealAmount);
        }
    }
}
