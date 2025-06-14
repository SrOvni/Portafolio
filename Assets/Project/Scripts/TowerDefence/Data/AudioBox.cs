using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Audio Box", menuName = "Scriptable Objects/Audio box")]
public class AudioBox : ScriptableObject
{
    [Serializable]
    public struct AudioParameters
    {
        public string? ClipName;
        public AudioClip[] AudioClips;
        [Range(0,1)] public float Volume;
        public float Pitch;
        public bool Loop;
        public float StartDelay;
    }
    public List<AudioParameters> Audios;
}
