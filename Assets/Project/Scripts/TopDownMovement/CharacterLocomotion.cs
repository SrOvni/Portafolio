using UnityEngine;

namespace TopDownGame
{
    class CharacterLocomotion
    {
        private readonly ICollisionResolver _collisionResolver;
        public readonly Capsule _capsule;
        public Vector3 Position { get; private set; }
        public CharacterLocomotion(ICollisionResolver collisionResolver, Capsule capsule, Vector3 startPosition)
        {
            _collisionResolver = collisionResolver;
            _capsule = capsule;
            Position = startPosition;
        }

        public void Move(Vector3 velocity)
        {
            Vector3 displacement = _collisionResolver.Resolver(Position, velocity, _capsule);

            Position += displacement;
        }

    }

}
