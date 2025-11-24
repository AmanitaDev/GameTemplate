using GameTemplate.Scripts.Systems.Audio;
using GameTemplate.Scripts.Systems.MVC;
using GameTemplate.Utils;
using UnityEngine;

namespace GameTemplate.Scripts.Systems.Settings
{
    public class SettingsModel : BaseModel
    {
        public float MusicVolume
        {
            get => UserPrefs.MusicVolume;
            private set => UserPrefs.MusicVolume = value;
        }
        
        public float EffectsVolume { 
            get => UserPrefs.EffectVolume;
            private set => UserPrefs.EffectVolume = value;
        }
        
        public int ResolutionIndex { 
            get => UserPrefs.ResolutionIndex;
            private set => UserPrefs.ResolutionIndex = value;
        }
        
        public bool IsFullscreen { 
            get => UserPrefs.IsFullscreen;
            private set => UserPrefs.IsFullscreen = value;
        }
        
        public bool UseVSync { 
            get => UserPrefs.UseVSync;
            private set => UserPrefs.UseVSync = value;
        }
        
        public int QualityLevel { 
            get => UserPrefs.QualityLevel;
            private set => UserPrefs.QualityLevel = value;
        }

        SettingsDataSO _settingsDataSo;
        AudioService _audioService;

        public SettingsModel(SettingsDataSO settingsDataSo, AudioService audioService)
        {
            _settingsDataSo = settingsDataSo;
            Debug.LogError("Constructing settings model");
            _audioService = audioService;
        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
            _audioService.SetMusicSourceVolume(volume);
        }

        public void SetEffectsVolume(float volume)
        {
            EffectsVolume = volume;
            _audioService.SetEffectsSourceVolume(volume);
        }

        public void SetQuality(int level)
        {
            QualityLevel = level;
            QualitySettings.SetQualityLevel(level);
        }

        public void SetResolution(int index)
        {
            ResolutionIndex = index;
        }

        public void SetFullscreen(bool isFullscreen)
        {
            IsFullscreen = isFullscreen;
            Screen.fullScreen = isFullscreen;
        }

        public void SetVSync(bool vsync)
        {
            UseVSync = vsync;
            QualitySettings.vSyncCount = vsync ? 1 : 0;
        }
    }
}