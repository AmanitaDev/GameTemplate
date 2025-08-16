using GameTemplate.Scripts.Systems.Audio;
using GameTemplate.Utils;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace GameTemplate.Systems.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioService
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
                _MusicSource.outputAudioMixerGroup = audioDataSo.audioMixer.FindMatchingGroups("Music")[0];
                Object.DontDestroyOnLoad(_MusicSource.gameObject);
            }
            
            if (_EffectSource == null)
            {
                var clone = Object.Instantiate(_audioDataSo.audioObject);
                clone.name = "Effects";
                _EffectSource = clone.GetComponent<AudioSource>();
                _EffectSource.volume = UserPrefs.EffectVolume;
                _EffectSource.outputAudioMixerGroup = audioDataSo.audioMixer.FindMatchingGroups("FX")[0];
                Object.DontDestroyOnLoad(_EffectSource.gameObject);
            }
        }

        public void StartMenuThemeMusic(bool restart)
        {
            PlayMusic(AudioID.MenuMusic, true, restart);
        }
        
        public void StartGameThemeMusic(bool restart)
        {
            PlayMusic(AudioID.GameMusic, true, restart);
        }

        public void PlaySFX(AudioID id)
        {
            if (_EffectSource == null)
            {
                Debug.LogError("Effect source is null!");
            }
            
            _EffectSource.clip = _audioDataSo.GetAudio(id);
            _EffectSource.Play();
        }

        private void PlayMusic(AudioID id, bool looping, bool restart)
        {
            if (_MusicSource == null)
            {
                Debug.LogError("Music source is null!");
            }
            
            if (_MusicSource.isPlaying)
            {
                // if we dont want to restart the clip do nothing
                if (!restart && _MusicSource.clip == _audioDataSo.GetAudio(id))
                    return;

                _MusicSource.Stop();
            }

            _MusicSource.clip = _audioDataSo.GetAudio(id);
            _MusicSource.loop = looping;
            _MusicSource.time = 0;
            _MusicSource.Play();
        }

        public void SetMusicSourceVolume(float volume)
        {
            _MusicSource.volume = volume;
        }
        
        public void SetEffectsSourceVolume(float volume)
        {
            _EffectSource.volume = volume;
        }
    }
}