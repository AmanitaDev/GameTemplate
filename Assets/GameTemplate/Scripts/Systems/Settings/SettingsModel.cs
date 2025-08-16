using GameTemplate.Scripts.Systems.MVC;
using GameTemplate.Systems.Audio;
using GameTemplate.Utils;
using UnityEngine;

namespace GameTemplate.Scripts.Systems.Settings
{
    public class SettingsModel : BaseModel
    {
        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }
        public int ResolutionIndex { get; private set; }
        public bool IsFullscreen { get; private set; }
        public bool UseVSync { get; private set; }
        public int QualityLevel { get; private set; }

        GameSettingsSO _config;
        AudioService _audioService;

        public SettingsModel(GameSettingsSO gameSettingsSo, AudioService audioService)
        {
            _config = gameSettingsSo;
            Debug.LogError("Constructing settings model");
            _audioService = audioService;
        }

        public override void Initialize()
        {
            MusicVolume = UserPrefs.MusicVolume;
            EffectsVolume = UserPrefs.EffectVolume;
            ResolutionIndex = UserPrefs.ResolutionIndex;
            IsFullscreen = UserPrefs.IsFullscreen;
            UseVSync = UserPrefs.UseVSync;
            QualityLevel = UserPrefs.QualityLevel;
        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
            _audioService.SetMusicSourceVolume(volume);
            UserPrefs.MusicVolume = volume;
        }

        public void SetEffectsVolume(float volume)
        {
            EffectsVolume = volume;
            _audioService.SetEffectsSourceVolume(volume);
            UserPrefs.EffectVolume = volume;
        }

        public void SetQuality(int level)
        {
            QualityLevel = level;
            QualitySettings.SetQualityLevel(level);
            UserPrefs.QualityLevel = level;
        }

        public void SetResolution(int index)
        {
            ResolutionIndex = index;
            UserPrefs.ResolutionIndex = index;
        }

        public void SetFullscreen(bool isFullscreen)
        {
            IsFullscreen = isFullscreen;
            Screen.fullScreen = isFullscreen;
            UserPrefs.IsFullscreen = isFullscreen;
        }

        public void SetVSync(bool vsync)
        {
            UseVSync = vsync;
            QualitySettings.vSyncCount = vsync ? 1 : 0;
            UserPrefs.UseVSync = vsync;
        }
    }
}