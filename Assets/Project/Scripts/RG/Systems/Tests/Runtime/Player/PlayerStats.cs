using System;
using UnityEngine;
using ImprovedTimers;

namespace RG.Systems.Tests.Player
{
    [Serializable]
    public class PlayerStats
    {
        public PlayerMovementData baseData;

        public float CurrentSpeed { get; private set; }

        public StopwatchTimer timePlayed = new StopwatchTimer();






    }
}
