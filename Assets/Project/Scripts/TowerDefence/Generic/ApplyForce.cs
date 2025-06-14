using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody; //Hola mundo
    [SerializeField] private  Vector3 _forceVector;
    [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
    [SerializeField] private float _xForce = 2;
    [SerializeField] private float _yMinForce = 6;
    [SerializeField] private float _yMaxForce = 3;
    [SerializeField] private float _zForce = 2;
    [SerializeField] private bool _applyForceOnAwake = true;
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.Log("No hay referencia para el rigidbody");
            return;
        }
        _forceVector.x = Random.Range(-_xForce, _xForce);
        _forceVector.y = Random.Range(_yMinForce, _yMaxForce);
        _forceVector.z = Random.Range(-_xForce, _zForce);
        if(_applyForceOnAwake)
        {
            ApplyForceToObject();
        }
    }
    public void ApplyForceToObject()
    {
        _rigidbody.AddForce(_forceVector, _forceMode);
    }
}
