using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine;
using Unity.VisualScripting;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public static AudioController Instance { get { return _instance; } set {_instance = value;}}
    [SerializeField] private AudioBox _audioBox;
    [SerializeField] private Logger _logger;
    [SerializeField] private List<AudioSource> _audioChannels;
    [SerializeField] private string _loggerName;
    [SerializeField] private List<AudioSource> _audioSourcesListPlaying = new List<AudioSource>();
    [SerializeField] private List<SaveAudioArgs> _saveAudioArgs = new List<SaveAudioArgs>();
    [SerializeField] private bool _savingAudios;
    [SerializeField] private bool _loadingAudios;
    private void Awake() {
        if(_instance == null)
        {
            _instance = this;
        }
        if(_logger == null)
        {
            _logger = GameObject.Find(_loggerName).GetComponent<Logger>();
        }
    }
    private void Start() {
        PlayAudio("BGM On");
        PlayAudio("BGM Off");
    }
    public void PlayAudio(string clip, float currentTimePlayBack = 0)
    {
        AudioBox.AudioParameters _audioParams = _audioBox.Audios.FirstOrDefault(a => a.ClipName == clip); // Enocntrar el clip a reproducir por el nombre del audio
        _logger.Log(_audioParams.ClipName);
        if(_audioParams.ClipName == null)return;
        AudioSource _audioSource = _audioChannels.FirstOrDefault(a => a.clip == null);
        if(_audioSource == null)return;
        _audioSource.clip = _audioParams.AudioClips[Random.Range(0, _audioParams.AudioClips.Length)]; 
        _audioSource.volume = _audioParams.Volume;
        _audioSource.pitch = _audioParams.Pitch;
        _audioSource.loop = _audioParams.Loop;
        _audioSource.PlayDelayed(_audioParams.StartDelay);
        _audioSource.time = currentTimePlayBack;
        StartCoroutine(ReleaseAudioChanel(_audioSource));
    }

    private IEnumerator ReleaseAudioChanel(AudioSource audioChannel)
    {
        yield return new WaitUntil(() => !audioChannel.isPlaying);
        audioChannel.clip = null;
    }
    private void OnApplicationPause(bool pauseStatus) {
        if(_savingAudios || _loadingAudios)return;
        if(pauseStatus)
        {
            
            StartCoroutine(SaveAudio());
            Debug.Log($"Saving audios: {_saveAudioArgs.Count}");
        }else{
            StartCoroutine(LoadAudio());
            Debug.Log($"Loading audios: {_saveAudioArgs.Count}");
        }
        
    }
    private IEnumerator SaveAudio()
    {
        _savingAudios = true;
            _saveAudioArgs.Clear();
            _audioSourcesListPlaying = _audioChannels.Where(a => a.clip != null).ToList();
            foreach (AudioSource audioSource in _audioSourcesListPlaying)
            {
                _saveAudioArgs.Add(new SaveAudioArgs() {
                    PlaybackStamp = audioSource.time,
                    AudioName = audioSource.clip.name
                });
                _audioChannels.Where(a => a.clip != null).ToList().ForEach(a => a = null);
            }
            yield return null;
            _savingAudios = false;
    }
    private IEnumerator LoadAudio()
    {
        _loadingAudios = true;
            foreach (SaveAudioArgs savedAudio in _saveAudioArgs)
            {
                PlayAudio(savedAudio.AudioName, savedAudio.PlaybackStamp);
                yield return null;
            }
            _loadingAudios = false;
    }
    public class SaveAudioArgs
    {
        public float PlaybackStamp;
        public string AudioName;
    }
}
