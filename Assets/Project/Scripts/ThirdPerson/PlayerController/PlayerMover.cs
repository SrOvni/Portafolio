using System;
using System.Threading;
using UnityEngine;
namespace AdvancedController
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerMover : MonoBehaviour
    {
        #region FIELDS 
        [Header("Collider settings")]
        [Range(0f, 1f)] [SerializeField] private float _stepHeightRatio = 0.2f;

        [SerializeField] private float _colliderHeight = 2f;
        [SerializeField] private float _colliderThickness = 1f;
        [SerializeField] private Vector3 _colliderOffset = Vector3.zero;

        Rigidbody rb;
        Transform tr;
        CapsuleCollider col;
        RaycastSensor sensor;

        bool isGrounded;
        float _baseSensorRange;
        Vector3 currentGroundAdjustmentVelocoty; //Velocity to adjust player position to maintain ground contact
        int currentLayer;

        [Header("Sensor Settings")]
        [SerializeField] private  bool _isInDebugMode;
        bool isUsingExtendedSensorRange = true; //Use extended range for smoother ground transistions
        #endregion
        const float SAFETY_DISTANCE_FACTOR = 0.001F; // Small factor added to prevent clipping issues when sensor range is calculated 
        void Awake()
        {
            Setup();
            RecalculateColliderDimmesions();

        }


        void OnValidate()
        {
            if(gameObject.activeInHierarchy)
            {
                RecalculateColliderDimmesions();
            }
        }
        public void CheckForGround()
        {
            if (currentLayer != gameObject.layer)
            {
                RecalculateSensorLayerMask();
            }

            currentGroundAdjustmentVelocoty = Vector3.zero;
            sensor.CastLenght = isUsingExtendedSensorRange ? _baseSensorRange + _colliderHeight * tr.localScale.x * _stepHeightRatio : _baseSensorRange;
            sensor.Cast();

            isGrounded = sensor.HasDetectedHit();
            if (!isGrounded) return;
            float distance = sensor.GetDistance();
            float upperLimit = _colliderHeight * tr.localPosition.x * (1f - _stepHeightRatio) * 0.5f;
            float middle = upperLimit + _colliderHeight * tr.localScale.x * _stepHeightRatio;
            float distanceToGo = middle - distance;

            currentGroundAdjustmentVelocoty = tr.up * (distanceToGo / Time.fixedDeltaTime); 
        }

        public bool IsGrounded() => isGrounded;
        public Vector3 GetGroundNormal() => sensor.GetNormal();

        public void SetVelocity(Vector3 velocity) => rb.linearVelocity = velocity + currentGroundAdjustmentVelocoty;

        public void SetExtendedSensorRange(bool isExtended) => isUsingExtendedSensorRange = isExtended;


        private void Setup()
        {
            tr = transform;
            rb = GetComponent<Rigidbody>();
            col = GetComponent<CapsuleCollider>();

            rb.freezeRotation = true;
            rb.useGravity = false;
        }
        private void RecalibrateSensor()
        {
            sensor ??= new RaycastSensor(tr);
            sensor.SetCastOrigin(col.bounds.center);
            sensor.SetCastDirection(RaycastSensor.CastDirection.Down);
            RecalculateSensorLayerMask();

            float length = _colliderHeight * (1f - _stepHeightRatio) * 0.5f + _colliderHeight * _stepHeightRatio;
            _baseSensorRange = length * (1f + SAFETY_DISTANCE_FACTOR) * tr.localScale.x;
            sensor.CastLenght = length * tr.localScale.x;

        }

        private void RecalculateSensorLayerMask()
        {
            int objectLayer = gameObject.layer;
            int layerMask = Physics.AllLayers;

            for(int i = 0; i < 32; i++)
            {
                if(Physics.GetIgnoreLayerCollision(objectLayer, i))
                {
                    layerMask &= ~(1 << i);
                }
            }

            int ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
            layerMask &= ~(1 << ignoreRaycastLayer);

            sensor.LayerMask = layerMask;
            currentLayer = objectLayer;
        }

        private void RecalculateColliderDimmesions()
        {
            if(col == null)
            {
                Setup();
            }
            col.height = _colliderHeight * (1 - _stepHeightRatio);
            col.radius = _colliderThickness / 2f;
            col.center = _colliderOffset * _colliderHeight + new Vector3(0f, _stepHeightRatio * col.height / 2f, 0f);

            if(col.height / 2f < col.radius)
            {
                col.radius = col.height /2f;
            }

            RecalibrateSensor();
        }

    }
}
