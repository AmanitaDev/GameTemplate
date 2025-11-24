using GameTemplate.Scripts.Systems.Audio;
using GameTemplate.Scripts.Systems.Pooling;
using GameTemplate.Systems.Pooling;

#if UNITY_EDITOR
using GameTemplate.Scripts.Systems.Scene;
using GameTemplate.Scripts.Systems.Settings;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
#endif

using UnityEngine;

namespace GameTemplate.Scripts.Editor
{
#if UNITY_EDITOR
    public class GameTemplateMenu : OdinMenuEditorWindow
    {
        [MenuItem("GameTemplate/Settings", false, 30)]
        private static void OpenWindow()
        {
            Debug.Log("GameTemplate Settings");
            GetWindow<GameTemplateMenu>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            Debug.Log("BuildMenuTree");
            var tree = new OdinMenuTree();

            // Load assets from path first
            var audioData = AssetDatabase.LoadAssetAtPath<AudioDataSO>("Assets/Resources/AudioData.asset");
            var poolingData = AssetDatabase.LoadAssetAtPath<PoolingDataSO>("Assets/Resources/PoolingData.asset");
            var sceneData = AssetDatabase.LoadAssetAtPath<SceneDataSO>("Assets/Resources/SceneData.asset");
            var settingsData = AssetDatabase.LoadAssetAtPath<SettingsDataSO>("Assets/Resources/SettingsData.asset");
            
            
            if (audioData != null)
                tree.Add("Audio Data", audioData);
            else
                Debug.LogWarning("AudioData.asset not found!");
            
            if (poolingData != null)
                tree.Add("Pooling Data", poolingData);
            else
                Debug.LogWarning("PoolingData.asset not found!");
            
            if (sceneData != null)
                tree.Add("Scene Data", sceneData);
            else
                Debug.LogWarning("SceneData.asset not found!");
            
            if (settingsData != null)
                tree.Add("Settings Data", settingsData);
            else
                Debug.LogWarning("Settings.asset not found!");

            return tree;
        }
    }
#endif
}