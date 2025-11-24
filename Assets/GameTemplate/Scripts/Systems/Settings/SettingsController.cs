using System.Collections.Generic;
using System.Linq;
using GameTemplate.Scripts.Systems.MVC;
using UnityEngine;

namespace GameTemplate.Scripts.Systems.Settings
{
    public class SettingsController : BaseController<SettingsModel, SettingsView>
    {
        public override void Initialize()
        {
            base.Initialize();
            Debug.Log("Initialized SettingsController");

            // Initialize view
            View.SetInitialValues(Model);

            // add resolution options depending on the hardware
            List<string> resolutionOptions = new List<string>();
            foreach (var resolution in Screen.resolutions.Reverse())
            {
                resolutionOptions.Add($"{resolution.width}x{resolution.height} - {resolution.refreshRateRatio}hz");
            }
            View.ResolutionDropdown.AddOptions(resolutionOptions);
            // find and set default resolution to the 1920x1080
            int id = Screen.resolutions.Select((item, i) => new { Item = item, Index = i })
                .First(x => x.Item is { width: 1920, height: 1080 }).Index;
            Model.SetResolution(id);

            // Bind buttons
            View.MusicSlider.onValueChanged.AddListener(Model.SetMusicVolume);
            View.EffectsSlider.onValueChanged.AddListener(Model.SetEffectsVolume);
            View.ResolutionDropdown.onValueChanged.AddListener(Model.SetResolution);
            View.FullscreenToggle.onValueChanged.AddListener(Model.SetFullscreen);
            View.VSyncToggle.onValueChanged.AddListener(Model.SetVSync);
            View.QualityDropdown.onValueChanged.AddListener(Model.SetQuality);

            Screen.SetResolution(Screen.resolutions[Model.ResolutionIndex].width,
                Screen.resolutions[Model.ResolutionIndex].height, Model.IsFullscreen);
            QualitySettings.SetQualityLevel(Model.QualityLevel);
        }
    }
}