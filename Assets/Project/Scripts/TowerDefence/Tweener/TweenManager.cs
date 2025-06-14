using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TweenManager : MonoBehaviour
{
    private static TweenManager _instance;
    private List<Action<float>> updateActions = new List<Action<float>>();
    private static bool _isApplicationQuitting = false;
    public static TweenManager Instance
    {
        get
        {
            if( _instance == null && !_isApplicationQuitting )
            {
                GameObject obj = new GameObject("TweenManager");
                _instance = obj.AddComponent<TweenManager>();
            }
            return _instance;
        }
    }
    private void Awake() {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update() {
        float deltaTime = Time.deltaTime;

        for (int i = 0; i < updateActions.Count; i++)
        {
            updateActions[i]?.Invoke(deltaTime);
        }
    }
    public void RegisterTween(Action<float> action)
    {
        if(_isApplicationQuitting) return;
        if(!updateActions.Contains(action))
        {
            updateActions.Add(action);
        }
    }

    public void UnregisterActions(Action<float> action)
    {
        if(updateActions.Contains(action))
        {
            updateActions.Remove(action);
        }
    }
    private void OnApplicationQuit() {
        _isApplicationQuitting = true;
    }
}
