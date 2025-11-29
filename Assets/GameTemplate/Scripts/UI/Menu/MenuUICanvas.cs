using GameTemplate.Scripts.Systems.Scene;
using GameTemplate.Scripts.Systems.Settings;
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
        SettingsController _settingsController;

        [Inject]
        public void Construct(ISceneService sceneService, SettingsController settingsController)
        {
            Debug.Log("Construct MenuUICanvas");
            _sceneService = sceneService;
            _settingsController = settingsController;

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

        public void SettingsButtonClick()
        {
            _settingsController.OpenCanvas();
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
                sceneEnum = SceneID.Game,
                unloadCurrent = true,
                activateLoadingCanvas = true,
                setActiveScene = true
            });
        }
    }
}