using System;
using UnityEngine;

namespace TopDownGame
{
    public class CameraRelativeMovement : IMovementDirectionProvider
    {
        private readonly Transform _camera;
        private readonly Func<Vector2> _input;

        public CameraRelativeMovement(Transform camera, Func<Vector2> input)
        {
            _camera = camera;
            _input = input;
        }
        public Vector3 GetMovementDirection()
        {
            Vector2 input = _input();
            Vector3 forward = _camera.forward;
            Vector3 right = _camera.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            return forward * input.y + right * input.x;
        }
    }

}
