using Unity.Cinemachine;
using UnityEngine;

namespace RG.Systems.Tests.Player
{
    public class CameraController : MonoBehaviour
    {
        Transform mainCamera;
        [SerializeField] CinemachineCamera _cinemachineCamera;
        private void Awake()
        {
            mainCamera = Camera.main.transform;
            _cinemachineCamera.Follow = transform;
            _cinemachineCamera.LookAt = transform;
            _cinemachineCamera.OnTargetObjectWarped(transform, transform.position - _cinemachineCamera.transform.position - Vector3.forward);
        }
    }
}
