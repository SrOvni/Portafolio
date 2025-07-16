using Patterns;
using UnityEngine;

namespace RG.Systems.Test
{
    public class LevelManager: Singleton<LevelManager>
    {
        [SerializeField] private LevelSO levelSO;
    }

}