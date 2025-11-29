using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameTemplate.Scripts.Systems.Scene
{
    [System.Serializable]
    public struct SceneNameData
    {
        public AssetReference scene;
        public string         sceneName;
        [HideInInspector]
        public SceneID sceneId;
    }
}