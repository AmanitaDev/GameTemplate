using GameTemplate.Scripts.Systems.SaveLoad;
using UnityEngine;

namespace GameTemplate.Scripts.UI.Game.EscapeMenu
{
    public class EscapeMenuPresenter
    {
        private EscapeMenuData model;
        private EscapeMenuView view;
        private SaveLoadManager saveLoadManager;

        public EscapeMenuPresenter(EscapeMenuView view)
        {
            this.view = view;
            model = new EscapeMenuData();
            saveLoadManager = new SaveLoadManager();

            // Bind buttons
            view.resumeButton.onClick.AddListener(ToggleMenu);
            view.saveButton.onClick.AddListener(SaveGame);
            view.loadButton.onClick.AddListener(LoadGame);
            view.quitButton.onClick.AddListener(QuitGame);

            // Hide menu initially
            view.ShowMenu(model.isMenuOpen);
        }

        public void ToggleMenu()
        {
            model.isMenuOpen = !model.isMenuOpen;
            view.ShowMenu(model.isMenuOpen);

            // Pause or unpause game
            Time.timeScale = model.isMenuOpen ? 0f : 1f;
        }

        private void SaveGame()
        {
            // Call your Save system here
            saveLoadManager.Save(new GameData()); // Replace with real player data
            Debug.Log("Game Saved!");
        }

        private void LoadGame()
        {
            // Call your Load system here
            GameData data = saveLoadManager.Load();
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