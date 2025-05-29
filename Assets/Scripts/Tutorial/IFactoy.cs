using System;
using UnityEngine;

public interface IFactoy {
    public GameObject Prefab { get; set;}
    public T Create<T>(Vector3 position) where T : MonoBehaviour;
}