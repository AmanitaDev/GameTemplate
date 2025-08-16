using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts.UI.Game.EscapeMenu
{
    public class EscapeMenuView : MonoBehaviour
    {
        public GameObject menuPanel;
        public Button resumeButton;
        public Button saveButton;
        public Button loadButton;
        public Button quitButton;

        public void ShowMenu(bool show)
        {
            if (menuPanel != null)
                menuPanel.SetActive(show);
        }
    }
}