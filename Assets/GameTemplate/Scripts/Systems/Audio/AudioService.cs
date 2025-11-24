using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameTemplate.Scripts.Systems.Pooling;
using GameTemplate.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameTemplate.Scripts.Systems.Audio
{
    /// <summary>
    /// Core application service responsible for managing all audio playback (Music, SFX).
    /// It uses VContainer for dependency injection and integrates with a PoolingService 
    /// for efficient handling of multiple simultaneous sound effects (SFX).
    /// </summary>
    public class AudioService
    {
        // Dedicated source for music and theme sounds. Kept as a single, long-lived source.
        AudioSource _musicSource;
        
        // Dependencies injected via VContainer
        AudioDataSO _audioDataSo;
        PoolingService _poolingService;

        /// <summary>
        /// VContainer Method Injection for mandatory service dependencies.
        /// This is where the core AudioSource GameObjects are fetched and configured.
        /// </summary>
        public AudioService(AudioDataSO audioDataSo, PoolingService poolingService)
        {
            Debug.Log("Construct AudioService");
            _audioDataSo = audioDataSo;
            _poolingService = poolingService;
            
            if (_musicSource == null)
            {
                var clone = _poolingService.GetGameObjectById(PoolID.AudioSource);
                clone.name = "Music";
                _musicSource = clone.GetComponent<AudioSource>();
                _musicSource.volume = UserPrefs.MusicVolume; 
                _musicSource.outputAudioMixerGroup = audioDataSo.audioMixer.FindMatchingGroups("Music")[0];
                Object.DontDestroyOnLoad(_musicSource.gameObject);
            }
        }
        
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Handles asynchronous service initialization, typically for Addressable or remote resource loading.
        /// </summary>
        public async Task InitializeAsync()
        {
            // Simulate loading music/audio clips
            await Task.Delay(500); // Replace with real audio loading
            IsInitialized = true;
            Debug.Log("AudioService initialized");
        }

        /// <summary>
        /// Plays a sound effect by fetching a temporary AudioSource from the pool.
        /// This allows for multiple simultaneous SFX playback without cutting off previous sounds.
        /// </summary>
        /// <param name="id">The unique identifier of the audio clip to play.</param>
        public void PlaySfx(AudioID id)
        {
            var audioClip = _audioDataSo.GetAudio(id);
            if (audioClip == null) return;
            
            // Get a source from the pool
            var pooledObject = _poolingService.GetGameObjectById(PoolID.AudioSource);
            var source = pooledObject.GetComponent<AudioSource>();

            if (source == null)
            {
                Debug.LogError("Pooled object is missing AudioSource component!");
                return;
            }

            // Configure and play the source
            source.clip = audioClip;
            source.volume = UserPrefs.EffectVolume; // Use the current volume setting
            source.loop = false; // SFX should generally not loop
            source.outputAudioMixerGroup = _audioDataSo.audioMixer.FindMatchingGroups("FX")[0];
            source.gameObject.SetActive(true);
            source.Play();
            
            // Schedule the source to be returned to the pool after the clip finishes
            float duration = audioClip.length;
            ReturnSourceAfterDelay(pooledObject, duration).Forget();
        }
        
        /// <summary>
        /// Asynchronously waits for the clip duration and returns the AudioSource back to the pool.
        /// </summary>
        private async UniTaskVoid ReturnSourceAfterDelay(GameObject pooledObject, float delay)
        {
            // Wait for the duration of the audio clip plus a small buffer
            await UniTask.Delay(TimeSpan.FromSeconds(delay), ignoreTimeScale: false);
            
            // Return the object to the pool
            pooledObject.SetActive(false);
        }

        /// <summary>
        /// Plays music on the dedicated music source, handling restart logic.
        /// </summary>
        public void PlayMusic(AudioID id, bool looping, bool restart)
        {
            if (_musicSource == null)
            {
                Debug.LogError("Music source is null!");
            }
            
            if (_musicSource.isPlaying)
            {
                // if we dont want to restart the clip do nothing
                if (!restart && _musicSource.clip == _audioDataSo.GetAudio(id))
                    return;

                _musicSource.Stop();
            }

            _musicSource.clip = _audioDataSo.GetAudio(id);
            _musicSource.loop = looping;
            _musicSource.time = 0;
            _musicSource.Play();
        }

        /// <summary>
        /// Sets the volume for the music track group.
        /// </summary>
        public void SetMusicSourceVolume(float volume)
        {
            _musicSource.volume = volume;
        }
    }
}