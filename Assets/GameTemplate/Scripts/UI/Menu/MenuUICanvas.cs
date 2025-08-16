using GameTemplate.Scripts.Systems.Scene;
using GameTemplate.Systems.Audio;
using GameTemplate.Utils;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using SceneLoadData = GameTemplate.Scripts.Systems.Scene.SceneLoadData;

namespace GameTemplate.Scripts.UI.Menu
{
    public class MenuUICanvas : MonoBehaviour
    {
        [SerializeField] Button ContinueButton;
        [SerializeField] GameObject ConfirmPanel;

        ISceneService _sceneService;
        AudioService _audioService;
        
        [Inject]
        public void Construct(ISceneService sceneLoader, AudioService audioService)
        {
            Debug.Log("Construct MenuUICanvas");
            _sceneService = sceneLoader;
            _audioService = audioService;

            ContinueButton.interactable = !UserPrefs.IsFirstPlay;
        }

        public void PlayButtonClick()
        {
            if (!UserPrefs.IsFirstPlay)
            {
                ConfirmPanel.SetActive(true);
                return;
            }

            UserPrefs.IsFirstPlay = false;
            LoadGameScene();
        }

        public void ContinueButtonClick()
        {
            LoadGameScene();
        }

        public void StartOverClick()
        {
            UserPrefs.DeleteAll();
            PlayButtonClick();
        }

        public void LoadGameScene()
        {
            _sceneService.LoadScene(new SceneLoadData
            {
                sceneName = "Game",
                unloadCurrent = true,
                activateLoadingCanvas = true,
                setActiveScene = true
            });
        }
    }
}