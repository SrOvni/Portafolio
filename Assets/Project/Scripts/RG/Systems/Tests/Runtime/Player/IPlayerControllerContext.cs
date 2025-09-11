
using UnityEngine;

namespace RG.Systems.Tests.Player
{
    public interface IPlayerControllerContext : IStateContext
    {
        GameObject player { get; }
        void HandleMovement();
    }

}