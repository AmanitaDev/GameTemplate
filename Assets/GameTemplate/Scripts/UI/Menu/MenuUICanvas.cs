using System.Collections.Generic;
using GameTemplate.Systems.Audio;
using GameTemplate.Systems.Scene;
using GameTemplate.Utils;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using SceneLoadData = GameTemplate.Systems.Scene.SceneLoadData;

namespace GameTemplate.Gameplay.UI
{
    public class MenuUICanvas : MonoBehaviour
    {
        [SerializeField] Button ContinueButton;
        [SerializeField] GameObject ConfirmPanel;

        ISceneService _SceneService;
        AudioService _audioService;
        
        [Inject]
        public void Construct(ISceneService sceneLoader, AudioService audioService)
        {
            Debug.Log("Construct MenuUICanvas");
            _SceneService = sceneLoader;
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
            _SceneService.LoadScene(new SceneLoadData
            {
                sceneName = "Game",
                unloadCurrent = true,
                activateLoadingCanvas = true,
                setActiveScene = true
            });
        }
    }
}