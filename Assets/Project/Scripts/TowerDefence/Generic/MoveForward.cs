using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    private void Update() {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }
}
