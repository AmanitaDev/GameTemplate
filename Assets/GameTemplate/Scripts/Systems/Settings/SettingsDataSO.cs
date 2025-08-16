using UnityEngine;

namespace GameTemplate.Scripts.Systems.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/Game Settings", order = 0)]
    public class SettingsDataSO : ScriptableObject
    {
        public float defaultMusicVolume = 0.8f;
        public float defaultEffectVolume = 0.8f;
        public int defaultResolutionIndex = 0;
        public bool defaultFullscreen = true;
        public bool defaultVSync = true;
        public int defaultQualityLevel = 2;
    }
}