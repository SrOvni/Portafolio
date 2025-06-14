using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestoyObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDestroy;
    [SerializeField] private float _delay = 0.1f;
    public void DestroyObjectWithDelay()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject, _delay);
    }
}
