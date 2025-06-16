using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // [SerializeField] CinemachineCamera _virtualCamera;

    [SerializeField] private CinemachineBasicMultiChannelPerlin _perlinNoise;

    [SerializeField] private float _noiseAmplitud;
    [SerializeField] private float _noiseFrequency;
    private void Start() {
        _perlinNoise.AmplitudeGain = 0;
        _perlinNoise.FrequencyGain = 0;
    }

    public void PlayShake(CameraShakeValues cameraShakeValues)
    {
        	_noiseAmplitud = cameraShakeValues.Amplitud;
            _noiseFrequency = cameraShakeValues.Frequency;
    }
    private void Update() {
        if(_noiseAmplitud > 0)
        {
            _perlinNoise.AmplitudeGain = _noiseAmplitud;
            _noiseAmplitud -= Time.deltaTime;
        }else{
            _perlinNoise.AmplitudeGain = 0;
        }
        if(_noiseFrequency > 0)
        {
            _perlinNoise.FrequencyGain = _noiseFrequency;
            _noiseFrequency -= Time.deltaTime;
        }else
        {
            _perlinNoise.FrequencyGain = 0;
        }
    }
}
