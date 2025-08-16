using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace GameTemplate.Scripts.Systems.Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/Audio data", order = 0)]
    public class AudioDataSO : SerializedScriptableObject
    {
        public GameObject audioObject;
        public AudioMixer audioMixer;

        [DictionaryDrawerSettings(KeyLabel = "AudioID", ValueLabel = "AudioClip")]
        public Dictionary<AudioID, AudioClip> AudioClips = new Dictionary<AudioID, AudioClip>();

        public AudioClip GetAudio(AudioID timesUp)
        {
            return AudioClips[timesUp];
        }
    }
}