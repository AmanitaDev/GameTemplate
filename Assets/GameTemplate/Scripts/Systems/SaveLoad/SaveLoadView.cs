using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts.Systems.SaveLoad
{
    public class SaveLoadView : MonoBehaviour
    {
        public Button saveButton;
        //public Button loadButton;

        public void UpdateUI(GameData data)
        {
            //playerNameInput.text = data.playerName;
            //levelText.text = $"Level: {data.level}";
            //experienceText.text = $"XP: {data.experience}";
        }
    }
}