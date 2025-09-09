using System;
using UnityEngine;

namespace RG.Systems.Tests.Player
{
    [Serializable]
    public class PlayerStats
    {
        public PlayerMovementData baseData;

        public float CurrentSpeed { get; private set; }

        public StopWatchTimer timePlayed = new StopWatchTimer();






    }
}
