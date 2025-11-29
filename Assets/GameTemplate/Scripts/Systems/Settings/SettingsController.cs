using System.Collections.Generic;
using System.Linq;
using GameTemplate.Scripts.Systems.Audio;
using UnityEngine;
using VContainer.Unity;

namespace GameTemplate.Scripts.Systems.Settings
{
    public class SettingsController : IInitializable
    {
        AudioService _audioService;
        SettingsModel _settingsModel;
        SettingsView _settingsView;
        
        public SettingsController(AudioService audioService, SettingsModel settingsModel, SettingsView settingsView)
        {
            Debug.Log("Construct settings controller");
            _audioService = audioService;
            _settingsModel = settingsModel;
            _settingsView = settingsView;
        }

        public void OpenCanvas()
        {
            _settingsView.ShowHideCanvas();
        }
        
        public void Initialize()
        {
            Debug.Log("Initialized SettingsController");

            // Initialize view
            _settingsView.SetInitialValues(_settingsModel);

            // add resolution options depending on the hardware
            List<string> resolutionOptions = new List<string>();
            foreach (var resolution in Screen.resolutions.Reverse())
            {
                resolutionOptions.Add($"{resolution.width}x{resolution.height} - {resolution.refreshRateRatio}hz");
            }
            _settingsView.InitResolutionDropdown(resolutionOptions);
            // find and set default resolution to the 1920x1080
            int id = Screen.resolutions.Select((item, i) => new { Item = item, Index = i })
                .First(x => x.Item is { width: 1920, height: 1080 }).Index;
            SetResolution(id);

            // Bind buttons
            _settingsView.MusicSlider.onValueChanged.AddListener(SetMusicVolume);
            _settingsView.EffectsSlider.onValueChanged.AddListener(SetEffectsVolume);
            _settingsView.ResolutionDropdown.onValueChanged.AddListener(SetResolution);
            _settingsView.FullscreenToggle.onValueChanged.AddListener(SetFullscreen);
            _settingsView.VSyncToggle.onValueChanged.AddListener(SetVSync);
            _settingsView.QualityDropdown.onValueChanged.AddListener(SetQuality);
            _settingsView.CloseButton.onClick.AddListener(_settingsView.ShowHideCanvas);

            Screen.SetResolution(Screen.resolutions[_settingsModel.ResolutionIndex].width,
                Screen.resolutions[_settingsModel.ResolutionIndex].height, _settingsModel.IsFullscreen);
            QualitySettings.SetQualityLevel(_settingsModel.QualityLevel);
        }
        
        public void SetMusicVolume(float volume)
        {
            _settingsModel.MusicVolume = volume;
            _audioService.SetMusicSourceVolume(volume);
        }

        public void SetEffectsVolume(float volume)
        {
            _settingsModel.EffectsVolume = volume;
        }

        public void SetQuality(int level)
        {
            _settingsModel.QualityLevel = level;
            QualitySettings.SetQualityLevel(level);
        }

        public void SetResolution(int index)
        {
            _settingsModel.ResolutionIndex = index;
        }

        public void SetFullscreen(bool isFullscreen)
        {
            _settingsModel.IsFullscreen = isFullscreen;
            Screen.fullScreen = isFullscreen;
        }

        public void SetVSync(bool vsync)
        {
            _settingsModel.UseVSync = vsync;
            QualitySettings.vSyncCount = vsync ? 1 : 0;
        }
    }
}