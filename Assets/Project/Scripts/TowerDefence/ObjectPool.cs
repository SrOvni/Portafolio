using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public GameObject ObjectPrefab;
    private GameObject _objectPoolGameObjectParent;
    private Stack<GameObject> _gameObjectsInPool = new Stack<GameObject>();
    public ObjectPool(GameObject prefab)
    {
        ObjectPrefab = prefab;
    }

    public GameObject GetGameObjectFromPool()
    {
        if ( _gameObjectsInPool.Count > 0)
        {
            return _gameObjectsInPool.Pop();
        }
        return CreateNewGameObject();
    }
    public GameObject CreateNewGameObject()
    {
        GameObject newGameObject = GameObject.Instantiate(ObjectPrefab);
        
        if(!newGameObject.TryGetComponent(out ReturnGameObjectToPool _ ))
        {
            newGameObject.AddComponent<ReturnGameObjectToPool>();
            newGameObject.GetComponent<ReturnGameObjectToPool>().ObjectPool = this;
            newGameObject.name = ObjectPrefab.name;
        }
        if(_objectPoolGameObjectParent == null)
        {
            _objectPoolGameObjectParent = new GameObject(ObjectPrefab.name + " parent");            
        }
            newGameObject.transform.SetParent(_objectPoolGameObjectParent.transform);
        return newGameObject;
    }

    public void ReturnGameObjectToPool(GameObject go)
    {
        _gameObjectsInPool.Push(go);
    }
}
