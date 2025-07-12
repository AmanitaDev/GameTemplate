using GameTemplate.Scripts.Systems.MVC;
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

        private readonly GameSettingsSO config;

        public SettingsModel(GameSettingsSO config)
        {
            this.config = config;
        }

        public override void Initialize()
        {
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", config.defaultMusicVolume);
            EffectsVolume = PlayerPrefs.GetFloat("EffectsVolume", config.defaultEffectVolume);
            ResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", config.defaultResolutionIndex);
            IsFullscreen = PlayerPrefs.GetInt("Fullscreen", config.defaultFullscreen ? 1 : 0) == 1;
            UseVSync = PlayerPrefs.GetInt("VSync", config.defaultVSync ? 1 : 0) == 1;
            QualityLevel = PlayerPrefs.GetInt("QualityLevel", config.defaultQualityLevel);
        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
        
        public void SetEffectsVolume(float volume)
        {
            EffectsVolume = volume;
            PlayerPrefs.SetFloat("EffectsVolume", volume);
        }
        
        public void SetQuality(int level)
        {
            QualityLevel = level;
            QualitySettings.SetQualityLevel(level);
            PlayerPrefs.SetInt("QualityLevel", level);
        }

        public void SetResolution(int index)
        {
            ResolutionIndex = index;
            PlayerPrefs.SetInt("ResolutionIndex", index);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            IsFullscreen = isFullscreen;
            Screen.fullScreen = isFullscreen;
            PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        }

        public void SetVSync(bool vsync)
        {
            UseVSync = vsync;
            QualitySettings.vSyncCount = vsync ? 1 : 0;
            PlayerPrefs.SetInt("VSync", vsync ? 1 : 0);
        }
    }
}