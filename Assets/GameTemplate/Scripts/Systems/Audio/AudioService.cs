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
        AudioSource _musicSource;
        //use for effect sounds
        AudioSource _effectSource;
        
        AudioDataSO _audioDataSo;
        private Transform holder;

        [Inject]
        public void Construct(AudioDataSO audioDataSo)
        {
            Debug.Log("Construct SoundService");
            _audioDataSo = audioDataSo;
            
            if (_musicSource == null)
            {
                var clone = Object.Instantiate(_audioDataSo.audioObject);
                clone.name = "Music";
                _musicSource = clone.GetComponent<AudioSource>();
                _musicSource.volume = UserPrefs.MusicVolume; 
                _musicSource.outputAudioMixerGroup = audioDataSo.audioMixer.FindMatchingGroups("Music")[0];
                Object.DontDestroyOnLoad(_musicSource.gameObject);
            }
            
            if (_effectSource == null)
            {
                var clone = Object.Instantiate(_audioDataSo.audioObject);
                clone.name = "Effects";
                _effectSource = clone.GetComponent<AudioSource>();
                _effectSource.volume = UserPrefs.EffectVolume;
                _effectSource.outputAudioMixerGroup = audioDataSo.audioMixer.FindMatchingGroups("FX")[0];
                Object.DontDestroyOnLoad(_effectSource.gameObject);
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

        public void PlaySfx(AudioID id)
        {
            if (_effectSource == null)
            {
                Debug.LogError("Effect source is null!");
            }
            
            _effectSource.clip = _audioDataSo.GetAudio(id);
            _effectSource.Play();
        }

        private void PlayMusic(AudioID id, bool looping, bool restart)
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

        public void SetMusicSourceVolume(float volume)
        {
            _musicSource.volume = volume;
        }
        
        public void SetEffectsSourceVolume(float volume)
        {
            _effectSource.volume = volume;
        }
    }
}