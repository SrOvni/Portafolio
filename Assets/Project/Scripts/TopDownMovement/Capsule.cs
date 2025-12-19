using UnityEngine;

namespace TopDownGame
{
    public struct Capsule
    {
        public float Radius;
        public float Height;
        Vector3 Center;
        public Capsule(float radius, float height, Vector3 center)
        {
            Radius = radius;
            Height = height;
            Center = center;
        }

        public void GetContactPoint(Vector3 position, out Vector3 bottom, out Vector3 top)
        {
            float halfHeight = Mathf.Max(0, Height * .5f - Radius);
            Vector3 up = Vector3.up * halfHeight;

            bottom = position + Center - up;
            top = position + Center + up;
        }
    }

}
