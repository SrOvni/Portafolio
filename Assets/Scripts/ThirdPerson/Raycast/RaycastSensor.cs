using UnityEngine;

namespace AdvancedController {
    public class RaycastSensor {
        public enum CastDirection {Forward, Right, Up, Backward, Left, Down}
        CastDirection castDirection;
        [SerializeField] private float _castLenght = 1f;
        [SerializeField] private LayerMask _layerMask = 255;
        Vector3 origin = Vector3.zero;
        Transform tr;
        RaycastHit hitInfo;
        public float CastLenght { get => _castLenght; set => _castLenght = value; }
        public LayerMask LayerMask { get => _layerMask; set => _layerMask = value; }

        public RaycastSensor(Transform originTransform)
        {
            tr = originTransform;
        }

        public void Cast()
        {
            Vector3 worldOrigin = tr.TransformPoint(origin);
            Vector3 worldDirection = GetCastDirection();

            Physics.Raycast(worldOrigin , worldDirection, out hitInfo, CastLenght, LayerMask, QueryTriggerInteraction.Ignore);

        }

        public bool HasDetectedHit() => hitInfo.collider != null;
        public float GetDistance() => hitInfo.distance;
        public Vector3 GetNormal() => hitInfo.normal;
        public Vector3 GetPosition() => hitInfo.point;
        public Collider GetCollider() => hitInfo.collider;

        public void SetCastDirection(CastDirection direction) => castDirection = direction;
        public void SetCastOrigin(Vector3 pos) => tr.InverseTransformPoint(pos);

        private Vector3 GetCastDirection()
        {
            return castDirection switch {
                CastDirection.Forward => tr.forward,
                CastDirection.Right => tr.right,
                CastDirection.Up => tr.up,
                CastDirection.Backward => tr.forward,
                CastDirection.Left => tr.right,
                CastDirection.Down => tr.up,
                _ => Vector3.one
            };
        }
    }
}
