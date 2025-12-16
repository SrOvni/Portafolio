using UnityEngine;

namespace RG.Systems.Tests.Player
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] LayerMask _groundLayers;
        [SerializeField, Min(0)] float _sphereRadius;
        [SerializeField] float _yAxisOffset;
        Vector3 Offset => new Vector3(0, _yAxisOffset, 0);
        public bool IsGrounded { get; private set; }
        void Update()
        {
            IsGrounded = Physics.SphereCast(transform.position + Offset, _sphereRadius, -transform.up, out RaycastHit hitInfo, _groundLayers);
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + Offset, _sphereRadius);
        }
    }
}
