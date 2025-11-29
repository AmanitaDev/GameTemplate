using System;
using GameTemplate.Scripts.Systems.Input;
using GameTemplate.Scripts.Systems.SaveLoad;
using GameTemplate.Scripts.Systems.Scene;
using GameTemplate.Scripts.Systems.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Scripts.UI.Game.EscapeMenu
{
    public class EscapeCanvasController: IStartable
    {
        public EscapeCanvasView _view;
        private SaveLoadSystem _saveLoadSystem;
        private SettingsController _settingsController;
        private ISceneService _sceneService;
        
        private Controls _controls;
        public bool _isMenuOpen = false;

        [Inject]
        public void Construct(Controls controls, EscapeCanvasView view, SaveLoadSystem saveLoadSystem,
            SettingsController settingsController, ISceneService sceneService)
        {
            _controls = controls;
            _controls.Enable();
            _view = view;
            _saveLoadSystem = saveLoadSystem;
            _settingsController = settingsController;
            _sceneService = sceneService;
        }
        
        public void Start()
        {
            // Bind buttons
            _view.resumeButton.onClick.AddListener(ToggleMenu);
            _view.menuButton.onClick.AddListener(ReturnMainMenu);
            _view.saveButton.onClick.AddListener(SaveGame);
            _view.loadButton.onClick.AddListener(LoadGame);
            _view.settingsButton.onClick.AddListener(_settingsController.OpenCanvas);
            _view.quitButton.onClick.AddListener(QuitGame);
            
            _controls.UI.Cancel.performed += ctx => ToggleMenu();
        }

        private void ToggleMenu()
        {
            _isMenuOpen = !_isMenuOpen;
            _view.ShowHideMenu(_isMenuOpen);
        }

        public void ReturnMainMenu()
        {
            _sceneService.LoadScene(new SceneLoadData
            {
                sceneEnum = SceneID.Menu,
                unloadCurrent = true,
                activateLoadingCanvas = true,
                setActiveScene = true
            });
        }

        private void SaveGame()
        {
            // Call your Save system here
            _saveLoadSystem.Save(new GameData()); // Replace with real player data
            Debug.Log("Game Saved!");
        }

        private void LoadGame()
        {
            // Call your Load system here
            GameData data = _saveLoadSystem.Load();
            Debug.Log($"Game Loaded");
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
    }
}