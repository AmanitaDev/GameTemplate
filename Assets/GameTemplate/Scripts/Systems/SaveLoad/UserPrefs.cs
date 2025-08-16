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
        const string firstPlayKey = "FirtPlay";

        public static bool IsFirstPlay
        {
            get => !PlayerPrefs.HasKey(firstPlayKey);
            set => PlayerPrefs.SetInt(firstPlayKey, value ? 1 : 0);
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        #region Settings Values

        const string musicVolumeKey = "MusicVolume";
        const string effectVolumeKey = "EffectVolume";
        const string resolutionIndexKey = "ResolutionIndex";
        const string isFullscreenKey = "IsFullscreen";
        const string useVSyncKey = "UseVSync";
        const string qualityLevelKey = "QualityLevel";

        public static float MusicVolume
        {
            get => PlayerPrefs.GetFloat(musicVolumeKey, .2f);
            set => PlayerPrefs.SetFloat(musicVolumeKey, value);
        }

        public static float EffectVolume
        {
            get => PlayerPrefs.GetFloat(effectVolumeKey, .2f);
            set => PlayerPrefs.SetFloat(effectVolumeKey, value);
        }

        public static int ResolutionIndex
        {
            get => PlayerPrefs.GetInt(resolutionIndexKey);
            set => PlayerPrefs.SetInt(resolutionIndexKey, value);
        }

        public static bool IsFullscreen
        {
            get => PlayerPrefs.GetInt(isFullscreenKey) == 1;
            set => PlayerPrefs.SetInt(isFullscreenKey, value ? 1 : 0);
        }

        public static bool UseVSync
        {
            get => PlayerPrefs.GetInt(useVSyncKey) == 1;
            set => PlayerPrefs.SetInt(useVSyncKey, value ? 1 : 0);
        }

        public static int QualityLevel
        {
            get => PlayerPrefs.GetInt(qualityLevelKey);
            set => PlayerPrefs.SetInt(qualityLevelKey, value);
        }

        #endregion
    }
}