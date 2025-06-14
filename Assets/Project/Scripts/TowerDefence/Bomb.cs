using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] private List<Transform> _damageList;
    [SerializeField] private string _objectToDamageTag;
    [SerializeField] private float _explotionTime;
    [SerializeField] private int _damagePower;
    [SerializeField] private UnityEvent OnDamageDealt;
    private void Start() {
        StartCoroutine(ExplotionRoutine());
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(_objectToDamageTag) && !_damageList.Contains(other.transform))
        {
            _damageList.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag(_objectToDamageTag))
        {
            _damageList.Remove(other.transform);
        }
        _damageList = _damageList.NotUnityNull().ToList();
    }
    IEnumerator ExplotionRoutine()
    {

        yield return new WaitForSeconds(_explotionTime);
        foreach(Transform weapon in _damageList)
        {
            if(weapon == null) continue;
            if(weapon.TryGetComponent(out Health component))
            {
                component.ReceiveDamage(_damagePower);
            }
        }
        OnDamageDealt?.Invoke();
        //Destroy(gameObject);
    }
}
