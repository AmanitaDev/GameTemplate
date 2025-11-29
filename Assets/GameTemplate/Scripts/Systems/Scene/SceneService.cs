using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace GameTemplate.Scripts.Systems.Scene
{
    public class SceneService : ISceneService
    {
        public event Action OnBeforeSceneLoad = delegate { };

        public event Action OnSceneLoaded = delegate { };

        private SceneDataSO _dataSo;
        public SceneInstance LastLoadedScene { get; private set; }

        [Inject]
        private void Construct(SceneDataSO dataSo)
        {
            Debug.Log("Construct SceneService");
            _dataSo = dataSo;

            Debug.LogError("Initialize SceneService");

            /*LoadScene(new SceneLoadData
            {
                sceneName = _data.nameOfSceneUIScene
            });*/
        }

        public void LoadMenuScene()
        {
            LoadScene(new SceneLoadData
            {
                sceneEnum = _dataSo.idOfSceneToLoadOnOpening,
                unloadCurrent = false,
                activateLoadingCanvas = true,
                setActiveScene = true
            });
        }

        public async void LoadScene(SceneLoadData sceneLoadData)
        {
            if (sceneLoadData.activateLoadingCanvas)
            {
                Debug.Log("Loading Scene");
                OnBeforeSceneLoad?.Invoke();
            }

            if (sceneLoadData.unloadCurrent)
                await UnloadScene();

            var sceneReference = _dataSo.GetSceneById(sceneLoadData.sceneEnum);

            var result = await Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Additive);

            LastLoadedScene = result;

            if (sceneLoadData.setActiveScene)
                SceneManager.SetActiveScene(LastLoadedScene.Scene);

            if (sceneLoadData.activateLoadingCanvas)
            {
                OnSceneLoaded.Invoke();
            }
        }

        public async UniTask UnloadScene()
        {
            await Addressables.UnloadSceneAsync(LastLoadedScene);
        }
    }
}