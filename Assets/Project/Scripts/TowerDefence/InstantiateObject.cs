using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private bool _useDifferentSpawnPoint = false;
    [SerializeField] private Transform _spawnPosition;
    [Range(0,1)][SerializeField] private float _spawnProbability = 1;
    public void CreateObject()
    {
        if(Random.value < _spawnProbability)
        {
            if(_useDifferentSpawnPoint)
            {
                Instantiate(_object, _spawnPosition.position ,Quaternion.identity);
                Debug.Log("Object instantiated");
            }else{
                Instantiate(_object,transform.position,Quaternion.identity);
                Debug.Log("Object instantiated");
            }
        }
    }
}
