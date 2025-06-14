using System;
using UnityEngine;

[CreateAssetMenu (fileName ="Waves Data", menuName = "Scriptable Objects/Create Waves Data")]
public class WaveData : ScriptableObject
{
    [Serializable]
    public struct Wave
    {
        public int WeakEnemies;
        public int MidEnemies;
        public int HeavyEnemies;
    }
    public Wave[] Waves;
}
