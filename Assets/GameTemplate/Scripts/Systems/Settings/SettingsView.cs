using GameTemplate.Scripts.Systems.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts.Systems.Settings
{
    public class SettingsView : BaseView
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private UISwitcher.UISwitcher fullscreenToggle;
        [SerializeField] private UISwitcher.UISwitcher vsyncToggle;

        public Slider MusicSlider => musicSlider;
        public Slider EffectsSlider => effectsSlider;
        public TMP_Dropdown ResolutionDropdown => resolutionDropdown;
        public UISwitcher.UISwitcher FullscreenToggle => fullscreenToggle;
        public UISwitcher.UISwitcher VSyncToggle => vsyncToggle;
        public TMP_Dropdown QualityDropdown => qualityDropdown;

        public void SetInitialValues(SettingsModel model)
        {
            musicSlider.value = model.MusicVolume;
            effectsSlider.value = model.EffectsVolume;
            qualityDropdown.value = model.QualityLevel;
            resolutionDropdown.value = model.ResolutionIndex;
            fullscreenToggle.isOn = model.IsFullscreen;
            vsyncToggle.isOn = model.UseVSync;
        }
    }
}