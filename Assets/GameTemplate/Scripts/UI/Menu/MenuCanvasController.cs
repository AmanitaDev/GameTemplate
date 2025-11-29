using GameTemplate.Scripts.Systems.Scene;
using GameTemplate.Scripts.Systems.Settings;
using GameTemplate.Scripts.Utils;
using GameTemplate.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Scripts.UI.Menu
{
    public class MenuCanvasController : IStartable
    {
        ISceneService _sceneService;
        SettingsController _settingsController;

        private MenuCanvasView _canvasView;

        [Inject]
        public void Construct(ISceneService sceneService, SettingsController settingsController, MenuCanvasView canvasView)
        {
            Debug.Log("Construct MenuUICanvas");
            _sceneService = sceneService;
            _settingsController = settingsController;
            _canvasView = canvasView;

            _canvasView.ChangeContinueButton(!UserPrefs.IsFirstPlay);
        }

        public void Start()
        {
            _canvasView.ContinueButton.AddClickListener(ContinueButtonClick);
            _canvasView.PlayButton.AddClickListener(PlayButtonClick);
            _canvasView.SettingsButton.AddClickListener(SettingsButtonClick);
            _canvasView.QuitButton.AddClickListener(QuitGame);
            
            _canvasView.ConfirmButton.AddClickListener(ConfirmButtonClick);
            _canvasView.CancelButton.AddClickListener(CancelButtonClick);
        }
        
        public void PlayButtonClick()
        {
            if (!UserPrefs.IsFirstPlay)
            {
                _canvasView.ChangeConfirmPanel(true);
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
            _canvasView.ChangeConfirmPanel(false);
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