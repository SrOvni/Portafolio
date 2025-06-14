using System;
using Random = UnityEngine.Random;
using UnityEngine;
using System.Runtime.InteropServices;
using System.ComponentModel;
using UnityEngine.Internal;
using UnityEditor.VersionControl;
[CreateAssetMenu(fileName = "AudioSFX", menuName = "Scriptable Objects/AudioSFX")]
public class AudioSfx: ScriptableObject
{
    [SerializeField] private float _currentAudioPlaybackPosition = 0;
    [Serializable]
    public struct AudioParametersStruct
    {
        public string ClipName;
        public AudioClip[] AudioClips;
        [Range(0,1)] public float Volume;
        public float Pitch;
        public bool Loop;
        public float StartDelay;
    }

    public AudioParametersStruct AudioParameters;
    [SerializeField] private AudioSource _audioSource = null;
    private GameObject _audioSourceGO;
    private bool _bussy;
    public void PlayAudio()
    {
        if(_audioSourceGO == null)
        {
            _audioSourceGO = new GameObject($"Audio {AudioParameters.ClipName}");
            _audioSourceGO.AddComponent<AudioSource>();
            _audioSource = _audioSourceGO.GetComponent<AudioSource>();
        }
        _audioSource.clip = AudioParameters.AudioClips[Random.Range(0, AudioParameters.AudioClips.Length)];
        _audioSource.volume = AudioParameters.Volume;
        _audioSource.pitch = AudioParameters.Pitch;
        _audioSource.loop = AudioParameters.Loop;
        _audioSource.PlayDelayed(AudioParameters.StartDelay);
    }
    private void OnApplicationPause(bool pauseStatus) {
        if(_bussy)return;
        _bussy = true;
        if(pauseStatus)
        {
            _currentAudioPlaybackPosition = _audioSource.time;
        }else
        {
            _audioSource.time = _currentAudioPlaybackPosition;
            _audioSource.Play();
        }
        _bussy = false;
    }
}
