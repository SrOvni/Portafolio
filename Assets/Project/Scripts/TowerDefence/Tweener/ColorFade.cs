using UnityEngine;
using UnityEngine.Events;

public class ColorFade : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    private Material _instanceMaterial;
    [SerializeField] private float _fadeDuration = .5f;
    [Range(0f,1f)]
    [SerializeField] private float _fadeValue = 1;
    [SerializeField] private UnityEvent<Material> _onFadedCompleted;
    [SerializeField] private bool _playOnAwake = false;
    private void Start() {
        if(_meshRenderer == null)_meshRenderer = GetComponent<MeshRenderer>();
        if(_playOnAwake)
        {
            FadeTo();
        }
    }
    public void FadeTo(Material material = null)
    {
        if(_meshRenderer == null)return;
        _instanceMaterial = (material != null)? material: _meshRenderer.material;
        _instanceMaterial.FadeTo(_fadeValue, _fadeDuration, this).onComplete(() => _onFadedCompleted?.Invoke(_instanceMaterial));
        
    }
    public void FadeTo()
    {
        if(_meshRenderer == null)return;
        _instanceMaterial = _meshRenderer.material;
        _instanceMaterial.FadeTo(_fadeValue, _fadeDuration, this).onComplete(() => _onFadedCompleted?.Invoke(_instanceMaterial));
        
    }
}
