using UnityEngine;

public class RotateTowardsMoveDirection : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 50;
    private Vector3 _previousPosition;
    private float _maxRadiansDelta = 1f;
    private void Update() {
        Vector3 currentDirection = transform.position - _previousPosition;
        Vector3 targetDirection = Vector3.RotateTowards(transform.forward, currentDirection, _maxRadiansDelta, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * _rotationSpeed);

        _previousPosition = transform.position; 
    }
}