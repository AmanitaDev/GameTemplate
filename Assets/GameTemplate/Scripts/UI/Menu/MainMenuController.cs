using GameTemplate.Scripts.Systems.Scene;
using GameTemplate.Scripts.Systems.Settings;
using GameTemplate.Scripts.Utils;
using GameTemplate.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Scripts.UI.Menu
{
    public class MainMenuController : IStartable
    {
        ISceneService _sceneService;
        SettingsController _settingsController;

        private MainMenuView _view;

        [Inject]
        public void Construct(ISceneService sceneService, SettingsController settingsController, MainMenuView view)
        {
            Debug.Log("Construct MenuUICanvas");
            _sceneService = sceneService;
            _settingsController = settingsController;
            _view = view;

            _view.ChangeContinueButton(!UserPrefs.IsFirstPlay);
        }

        public void Start()
        {
            _view.ContinueButton.AddClickListener(ContinueButtonClick);
            _view.PlayButton.AddClickListener(PlayButtonClick);
            _view.SettingsButton.AddClickListener(SettingsButtonClick);
            _view.QuitButton.AddClickListener(QuitGame);
            
            _view.ConfirmButton.AddClickListener(ConfirmButtonClick);
            _view.CancelButton.AddClickListener(CancelButtonClick);
        }
        
        public void PlayButtonClick()
        {
            if (!UserPrefs.IsFirstPlay)
            {
                _view.ChangeConfirmPanel(true);
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
        
        private void QuitGame()
        {
            Debug.Log("Quit Game");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void ConfirmButtonClick()
        {
            UserPrefs.DeleteAll();
            PlayButtonClick();
        }
        
        public void CancelButtonClick()
        {
            _view.ChangeConfirmPanel(false);
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