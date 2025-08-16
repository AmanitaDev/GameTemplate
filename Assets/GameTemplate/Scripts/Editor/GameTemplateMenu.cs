using GameTemplate.Scripts.Systems.Audio;
using GameTemplate.Systems.Pooling;

#if UNITY_EDITOR
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
            tree.AddAssetAtPath("Editor Game Settings", "Assets/Resources/EditorGameSettings.asset",
                typeof(EditorGameSettings));
            tree.AddAssetAtPath("Pooling Data", "Assets/Resources/Data/PoolingData.asset", typeof(PoolingDataSO));
            tree.AddAssetAtPath("Audio Data", "Assets/Resources/Data/AudioData.asset", typeof(AudioDataSO));
            return tree;
        }
    }
#endif
}