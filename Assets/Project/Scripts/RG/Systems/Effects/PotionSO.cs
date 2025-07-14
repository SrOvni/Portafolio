using UnityEngine;
using UnityEditor;

namespace RG.Systems.Effects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "PotionSO", menuName = "PotionSO", order = 0)]
    public class PotionSO : ScriptableObject
    {
        [SerializeField] private string potionName;
        [SerializeField] private Sprite icon;
        [SerializeField] private EffectSO<ICharacter>[] effects;
        private IAction<ICharacter>[] _cachedEffects;

        public string PotionName => potionName;
        public Sprite Icon => icon;

        void OnEnable()
        {
            _cachedEffects = new IAction<ICharacter>[effects.Length];
            for (int i = 0; i < effects.Length; i++)
            {
                _cachedEffects[i] = effects[i];
            }
        }

        public void ApplyEffects(ICharacter target)
        {
            foreach (var effect in _cachedEffects)
            {
                effect?.Apply(target);
            }
        }
    }
}
