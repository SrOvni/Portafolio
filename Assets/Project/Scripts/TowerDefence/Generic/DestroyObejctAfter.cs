using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObejctAfter : MonoBehaviour
{
    [SerializeField] private float _secondsToDestroyObject = 5;
    private void Start() {
        Destroy(gameObject, _secondsToDestroyObject);
    }
}
