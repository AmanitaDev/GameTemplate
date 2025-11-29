using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts.Systems.Settings
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private UISwitcher.UISwitcher fullscreenToggle;
        [SerializeField] private UISwitcher.UISwitcher vsyncToggle;
        [SerializeField] private Button closeButton;

        public Slider MusicSlider => musicSlider;
        public Slider EffectsSlider => effectsSlider;
        public TMP_Dropdown ResolutionDropdown => resolutionDropdown;
        public UISwitcher.UISwitcher FullscreenToggle => fullscreenToggle;
        public UISwitcher.UISwitcher VSyncToggle => vsyncToggle;
        public TMP_Dropdown QualityDropdown => qualityDropdown;
        public Button CloseButton => closeButton;
        
        public CanvasGroup _canvasGroup;
        
        private bool _isOpen = false;

        public void SetInitialValues(SettingsModel model)
        {
            musicSlider.value = model.MusicVolume;
            effectsSlider.value = model.EffectsVolume;
            qualityDropdown.value = model.QualityLevel;
            resolutionDropdown.value = model.ResolutionIndex;
            fullscreenToggle.isOn = model.IsFullscreen;
            vsyncToggle.isOn = model.UseVSync;
        }
        
        public void InitResolutionDropdown(List<string> resolutionOptions)
        {
            resolutionDropdown.AddOptions(resolutionOptions);
        }
        
        public void ShowHideCanvas()
        {
            _isOpen = !_isOpen;
            _canvasGroup.alpha = _isOpen ? 1 : 0;
            _canvasGroup.interactable = _isOpen;
            _canvasGroup.blocksRaycasts = _isOpen;
        }
    }
}