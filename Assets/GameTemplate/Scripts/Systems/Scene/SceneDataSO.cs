using System;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace GameTemplate.Scripts.Systems.Scene
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/SceneData")]
    public class SceneDataSO : ScriptableObject
    {
        [SerializeField] private SceneNameData[] sceneData;

        public SceneID idOfSceneToLoadOnOpening;

        public AssetReference GetSceneById(SceneID sceneId)
        {
            foreach (var sceneData in sceneData)
            {
                if (sceneData.sceneId == sceneId)
                    return sceneData.scene;
            }

            throw new NullReferenceException($"Could not find the scene reference with name {sceneId}.");
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            for (int i = 0; i < sceneData.Length; i++)
            {
                var editorAsset = sceneData[i].scene.editorAsset;
                sceneData[i].sceneName = editorAsset ? editorAsset.name : "";
                sceneData[i].sceneId = (SceneID)Enum.Parse(typeof(SceneID), sceneData[i].sceneName);
            }
#endif
        }

#if UNITY_EDITOR
        [Button("Apply Scenes")]
        public void Generate()
        {
            string filePathAndName =
                "Assets/GameTemplate/Scripts/Systems/Scene/SceneID.cs"; //The folder Scripts/Enums/ is expected to exist

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                streamWriter.WriteLine("public enum SceneID");
                streamWriter.WriteLine("{");
                for (int i = 0; i < sceneData.Length; i++)
                {
                    streamWriter.WriteLine("\t" + sceneData[i].sceneName + ",");
                }

                streamWriter.WriteLine("}");
            }

            AssetDatabase.Refresh();
        }
#endif
    }
}