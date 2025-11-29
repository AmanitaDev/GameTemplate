using GameTemplate.Scripts.Systems.Audio;
using GameTemplate.Utils;
using UnityEngine;

namespace GameTemplate.Scripts.Systems.Settings
{
    public class SettingsModel
    {
        public float MusicVolume
        {
            get => UserPrefs.MusicVolume;
            set => UserPrefs.MusicVolume = value;
        }

        public float EffectsVolume
        {
            get => UserPrefs.EffectVolume;
            set => UserPrefs.EffectVolume = value;
        }

        public int ResolutionIndex
        {
            get => UserPrefs.ResolutionIndex;
            set => UserPrefs.ResolutionIndex = value;
        }

        public bool IsFullscreen
        {
            get => UserPrefs.IsFullscreen;
            set => UserPrefs.IsFullscreen = value;
        }

        public bool UseVSync
        {
            get => UserPrefs.UseVSync;
            set => UserPrefs.UseVSync = value;
        }

        public int QualityLevel
        {
            get => UserPrefs.QualityLevel;
            set => UserPrefs.QualityLevel = value;
        }

        SettingsDataSO _settingsDataSo;
        AudioService _audioService;

        public SettingsModel(SettingsDataSO settingsDataSo)
        {
            _settingsDataSo = settingsDataSo;
            Debug.LogError("Constructing settings model");

            if (UserPrefs.IsFirstPlay)
            {
                //set default values for settings
                MusicVolume = _settingsDataSo.defaultMusicVolume;
                EffectsVolume = _settingsDataSo.defaultEffectVolume;
                ResolutionIndex = _settingsDataSo.defaultResolutionIndex;
                IsFullscreen = _settingsDataSo.defaultFullscreen;
                UseVSync = _settingsDataSo.defaultVSync;
                QualityLevel = _settingsDataSo.defaultQualityLevel;
            }
        }
    }
}