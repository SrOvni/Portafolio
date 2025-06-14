using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScaleObject : MonoBehaviour
{
    [SerializeField] private Vector3 _scaleObjectTo = Vector3.one;
    [SerializeField] private float _scaleDuration = 1;
    [SerializeField] private bool _scaleOnStart = false;
    Tween tween;
    private void Start() {
        if(_scaleOnStart)
        {
            ApplyScale();
        }
    }
    public void ApplyScale()
    {
        tween = transform.ScaleTo(_scaleObjectTo, _scaleDuration);
    }
    private void OnDestroy() {
        tween.Kill();
    }
}
