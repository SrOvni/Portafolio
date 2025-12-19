using UnityEngine;

namespace TopDownGame
{
    public interface ICollisionResolver
    {
        public Vector3 Resolver(Vector3 position, Vector3 velocity, Capsule collider);
    }

}
