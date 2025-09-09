
using UnityEngine;

namespace RG.Systems
{
    public interface IPlayerControllerContext
    {
        GameObject player { get; }
        void HandleMovement();

        //StopwatchTimer StunTimer;

        //StunTimer.Start();
        //StunTimer.Stop();
    }

}