using System;
using DG.Tweening;
using GameTemplate.Gameplay.UI;
using GameTemplate.Utils;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace GameTemplate.Systems.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundService
    {
        //use for music and theme sounds
        AudioSource _MusicSource;
        //use for effect sounds
        AudioSource _EffectSource;
        
        AudioDataSO _audioDataSo;
        private Transform _Holder;

        [Inject]
        public void Construct(AudioDataSO audioDataSo)
        {
            Debug.Log("Construct SoundService");
            _audioDataSo = audioDataSo;
            
            if (_MusicSource == null)
            {
                var clone = Object.Instantiate(_audioDataSo.audioObject);
                clone.name = "Music";
                _MusicSource = clone.GetComponent<AudioSource>();
                _MusicSource.volume = UserPrefs.MusicVolume; 
                Object.DontDestroyOnLoad(_MusicSource.gameObject);
            }
            
            if (_EffectSource == null)
            {
                var clone = Object.Instantiate(_audioDataSo.audioObject);
                clone.name = "Effects";
                _EffectSource = clone.GetComponent<AudioSource>();
                _EffectSource.volume = UserPrefs.EffectVolume; 
                Object.DontDestroyOnLoad(_EffectSource.gameObject);
            }
        }

        public void StartMenuThemeMusic(bool restart)
        {
            PlayTrack(_audioDataSo.GetAudio(AudioID.MenuMusic), true, restart);
        }
        public void StartGameThemeMusic(int orderId)
        {
            AudioClip firstClip = _audioDataSo.GetMusicPlayerMusics(orderId);
            PlayTrack(firstClip, true, true);
        }

        public void PlayWinSound()
        {
            PlaySound(_audioDataSo.GetAudio(AudioID.Win));
        }

        public void PlayLoseSound()
        {
            PlaySound(_audioDataSo.GetAudio(AudioID.Lose));
        }

        private void PlaySound(AudioClip clip)
        {
            if (_EffectSource == null)
            {
                Debug.LogError("Effect source is null!");
            }

            _EffectSource.clip = clip;
            _EffectSource.Play();
        }

        private void PlayTrack(AudioClip clip, bool looping, bool restart)
        {
            if (_MusicSource == null)
            {
                Debug.LogError("Music source is null!");
            }
            
            if (_MusicSource.isPlaying)
            {
                // if we dont want to restart the clip, do nothing if it is playing
                if (!restart && _MusicSource.clip == clip)
                {
                    return;
                }

                _MusicSource.Stop();
            }

            _MusicSource.clip = clip;
            _MusicSource.loop = looping;
            _MusicSource.time = 0;
            _MusicSource.Play();
        }

        public void SetMusicVolume(float volume)
        {
            _MusicSource.volume = volume;
        }
        
        public void SetEffectsVolume(float volume)
        {
            _EffectSource.volume = volume;
        }
    }
}