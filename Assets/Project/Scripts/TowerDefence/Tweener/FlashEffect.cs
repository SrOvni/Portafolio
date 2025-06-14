using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] private Logger _logger;
    [SerializeField] private Color _initialColor;
    [SerializeField] private Color _flashColor;
    [SerializeField] private float _flashDuration;
    [SerializeField] private string _loggerName;
    [SerializeField] private bool _playOnAwake = false;
    private Tween tween;
    [SerializeField] private UnityEvent _onFlashCompleted;
    private void Start()
    {
        if(_logger == null)
        {
            if(_loggerName == null)Debug.LogWarning($"No logger name provided to object {gameObject.name}");
            _logger = GameObject.Find(_loggerName).GetComponent<Logger>();
        }
        if(meshRenderer == null)meshRenderer = GetComponentInChildren<MeshRenderer>();

        if(_playOnAwake)
        {
            print("Flashing on awake");
            PlayFlashEffect();
        }

    }
    public void PlayFlashEffect()
    {
        if(meshRenderer == null)
        {
            _logger.Log("No hay Referencia del meshrenderer en: " + gameObject.name);
            return;
        }
        if(_initialColor == null || _flashColor == null)
        {
            _logger.Log("No hay referencia de los colores para aplicar efecto al objeto: " + gameObject.name);
            return;
        }
        Material material = meshRenderer.material;
        tween = material.FadeTo(_flashColor, _flashDuration/2).onComplete(() => material.FadeTo(_initialColor, _flashDuration/2).onComplete(() => _onFlashCompleted?.Invoke()));
    }
    private void OnDestroy() {
        if(tween != null)
        {
                _logger.Log($"{transform.name} destroyed while tweening {this.GetType().GetTypeInfo()}");
            tween.Kill();
        }
    }
}
