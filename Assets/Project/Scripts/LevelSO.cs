using UnityEngine;

namespace RG.Systems
{
    [CreateAssetMenu(fileName = "ObjectiveData", menuName = "Level/ New Level", order = 0)]
    public class LevelSO : ScriptableObject
    {
        public string MissionName;
        public string Description;
        public ObjectiveData[] Objectives;
    }
}
