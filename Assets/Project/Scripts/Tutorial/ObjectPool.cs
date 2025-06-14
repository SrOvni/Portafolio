using System;
using System.Collections.Generic;
using EnemyBaseClass;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour {
    private readonly Func<T> _onCreate;
    private readonly Action<T> _onGet;
    private readonly Action<T> _onRelease;
    private readonly Action<T> _onDestroy;
    private readonly bool _colectionCheck;
    private readonly List<T> objsList;
    UnityEngine.Pool.ObjectPool<Enemy> objectPool;

    
    public ObjectPool(Func<T> onCreate, Action<T> onGet, Action<T> onRelease, Action<T> onDestroy, bool colectionCheck = true, int defaultSize = 10, int maxSize = 10000)
    {
        objsList = new List<T>(defaultSize);
        _onCreate = onCreate;
        _onGet = onGet;
        _onRelease = onRelease;
        _onDestroy = onDestroy;
        _colectionCheck = colectionCheck;
    }
    
    
}