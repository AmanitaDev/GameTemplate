using UnityEngine;

namespace GameTemplate.Scripts.Systems.SaveLoad
{
    public class SaveLoadPresenter
    {
        private GameData model;
        private SaveLoadView view;
        private SaveLoadManager manager;

        public SaveLoadPresenter(SaveLoadView view)
        {
            this.view = view;
            manager = new SaveLoadManager();

            // Bind buttons
            view.saveButton.onClick.AddListener(SaveGame);
            //view.loadButton.onClick.AddListener(LoadGame);

            // Initialize model
            model = new GameData();
            view.UpdateUI(model);
        }

        public void SaveGame()
        {
            //model.playerName = view.playerNameInput.text;
            manager.Save(model);
        }

        public void LoadGame()
        {
            model = manager.Load();
            view.UpdateUI(model);
        }
    }
}