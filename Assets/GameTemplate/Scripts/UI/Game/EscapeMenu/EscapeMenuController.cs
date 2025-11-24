using GameTemplate.Scripts.Systems.Input;
using GameTemplate.Scripts.Systems.SaveLoad;
using UnityEngine;
using VContainer.Unity;

namespace GameTemplate.Scripts.UI.Game.EscapeMenu
{
    public class EscapeMenuController: IInitializable
    {
        public EscapeMenuView _view;
        private SaveLoadSystem _saveLoadSystem;
        
        private Controls _controls;
        public bool _isMenuOpen = false;

        // Dependencies are injected via the constructor
        public EscapeMenuController(Controls controls, EscapeMenuView view, SaveLoadSystem saveLoadSystem)
        {
            _controls = controls;
            _controls.Enable();
            _view = view;
            _saveLoadSystem = saveLoadSystem;
            
            // Bind buttons
            _view.resumeButton.onClick.AddListener(ToggleMenu);
            _view.saveButton.onClick.AddListener(SaveGame);
            _view.loadButton.onClick.AddListener(LoadGame);
            _view.quitButton.onClick.AddListener(QuitGame);
            Debug.LogError("EscapeMenuController::ShowHideMenu() called");
            _controls.UI.Cancel.performed += ctx => ToggleMenu();
        }
        
        public void Initialize()
        {
            
        }

        private void ToggleMenu()
        {
            _isMenuOpen = !_isMenuOpen;
            _view.ShowHideMenu(_isMenuOpen);
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