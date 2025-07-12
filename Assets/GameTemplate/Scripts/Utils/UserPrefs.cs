using UnityEngine;

namespace GameTemplate.Utils
{
    /// <summary>
    /// Singleton class which saves/loads local settings.
    /// (This is just a wrapper around the PlayerPrefs system,
    /// so that all the calls are in the same place.)
    /// </summary>
    public static class UserPrefs
    {
        const string k_FirstPlayKey = "FirtPlay";

        public static bool IsFirstPlay
        {
            get => !PlayerPrefs.HasKey(k_FirstPlayKey);
            set => PlayerPrefs.SetInt(k_FirstPlayKey, value ? 1 : 0);
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        #region Settings Values

        const string k_MusicVolumeKey = "MusicVolume";
        const string k_EffectVolumeKey = "EffectVolume";
        const string k_ResolutionIndexKey = "ResolutionIndex";
        const string k_IsFullscreenKey = "IsFullscreen";
        const string k_UseVSyncKey = "UseVSync";
        const string k_QualityLevelKey = "QualityLevel";

        public static float MusicVolume
        {
            get => PlayerPrefs.GetFloat(k_MusicVolumeKey, .2f);
            set => PlayerPrefs.SetFloat(k_MusicVolumeKey, value);
        }

        public static float EffectVolume
        {
            get => PlayerPrefs.GetFloat(k_EffectVolumeKey, .2f);
            set => PlayerPrefs.SetFloat(k_EffectVolumeKey, value);
        }

        public static int ResolutionIndex
        {
            get => PlayerPrefs.GetInt(k_ResolutionIndexKey);
            set => PlayerPrefs.SetInt(k_ResolutionIndexKey, value);
        }

        public static bool IsFullscreen
        {
            get => PlayerPrefs.GetInt(k_IsFullscreenKey) == 1;
            set => PlayerPrefs.SetInt(k_IsFullscreenKey, value ? 1 : 0);
        }

        public static bool UseVSync
        {
            get => PlayerPrefs.GetInt(k_UseVSyncKey) == 1;
            set => PlayerPrefs.SetInt(k_UseVSyncKey, value ? 1 : 0);
        }

        public static int QualityLevel
        {
            get => PlayerPrefs.GetInt(k_QualityLevelKey);
            set => PlayerPrefs.SetInt(k_QualityLevelKey, value);
        }

        #endregion
    }
}