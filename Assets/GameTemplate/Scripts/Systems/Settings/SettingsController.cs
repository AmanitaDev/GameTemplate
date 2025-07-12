using System.Collections.Generic;
using System.Linq;
using GameTemplate.Scripts.Systems.MVC;
using UnityEngine;
using VContainer;

namespace GameTemplate.Scripts.Systems.Settings
{
    public class SettingsController : BaseController<SettingsModel, SettingsView>
    {
        public SettingsController(SettingsModel model, SettingsView view) : base(model, view) { }
        
        public override void Initialize()
        {
            base.Initialize();

            view.SetInitialValues(model);
            
            List<string> resolutionOptions = new List<string>();
            foreach (var resolution in Screen.resolutions.Reverse())
            {
                resolutionOptions.Add($"{resolution.width}x{resolution.height}");
            }
            view.ResolutionDropdown.AddOptions(resolutionOptions);
            int id = Screen.resolutions.Select((item, i) => new { Item = item, Index = i })
                .First(x => x.Item is { width: 1920, height: 1080 }).Index;
            model.SetResolution(id);

            view.MusicSlider.onValueChanged.AddListener(model.SetMusicVolume);
            view.EffectsSlider.onValueChanged.AddListener(model.SetEffectsVolume);
            view.ResolutionDropdown.onValueChanged.AddListener(model.SetResolution);
            view.FullscreenToggle.onValueChanged.AddListener(model.SetFullscreen);
            view.VSyncToggle.onValueChanged.AddListener(model.SetVSync);
            view.QualityDropdown.onValueChanged.AddListener(model.SetQuality);

            //TODO
            //AudioListener.volume = model.MasterVolume;
            Screen.SetResolution(Screen.resolutions[model.ResolutionIndex].width, Screen.resolutions[model.ResolutionIndex].height, model.IsFullscreen);
            QualitySettings.SetQualityLevel(model.QualityLevel);
        }
    }
}