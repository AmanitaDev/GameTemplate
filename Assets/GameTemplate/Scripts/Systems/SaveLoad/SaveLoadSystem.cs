using System.IO;
using UnityEngine;

namespace GameTemplate.Scripts.Systems.SaveLoad
{
    public class SaveLoadSystem
    {
        private static string savePath => Path.Combine(Application.persistentDataPath, "save.json");

        public void Save(GameData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(savePath, json);
            Debug.Log($"Game Saved to {savePath}");
        }

        public GameData Load()
        {
            if (!File.Exists(savePath))
            {
                Debug.LogWarning("No save file found. Returning new data.");
                return new GameData();
            }

            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log($"Game Loaded from {savePath}");
            return data;
        }

        public void DeleteSave()
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                Debug.Log("Save file deleted.");
            }
        }
    }
}