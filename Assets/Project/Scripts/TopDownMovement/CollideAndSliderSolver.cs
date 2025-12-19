using UnityEngine;

namespace TopDownGame
{
    public class CollideAndSliderSolver : ICollisionResolver
    {
        private const int MaxIterations = 5;
        private const float SkinWidth = 0.01f;

        public Vector3 Resolver(Vector3 position, Vector3 velocity, Capsule capsule)
        {
            Vector3 remaining = velocity;
            Vector3 currentPosition = position;

            for (int i = 0; i < MaxIterations; i++)
            {
                if (remaining.sqrMagnitude < 0.0001f)
                    break;
                capsule.GetContactPoint(currentPosition, out Vector3 bottom, out Vector3 top);
                if(Physics.CapsuleCast(bottom, top, capsule.Radius, remaining.normalized, out RaycastHit hit, remaining.magnitude + SkinWidth))
                {
                    float distance = Mathf.Max(hit.distance - SkinWidth, 0);
                    Vector3 moveToHit = remaining.normalized * distance;
                    currentPosition += moveToHit;
                    Vector3 normal = hit.normal;
                    remaining -= moveToHit;
                    
                    remaining = Vector3.ProjectOnPlane(remaining, normal);
                }
                else
                {
                    currentPosition += remaining;
                    break;
                }

            }
            return currentPosition - position;
        }
    }

}
