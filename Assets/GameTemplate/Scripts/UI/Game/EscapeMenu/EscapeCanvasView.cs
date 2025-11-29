using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts.UI.Game.EscapeMenu
{
    public class EscapeCanvasView : MonoBehaviour
    {
        public GameObject menuPanel;
        public Button resumeButton;
        public Button menuButton;
        public Button saveButton;
        public Button loadButton;
        public Button settingsButton;
        public Button quitButton;
        
        public void ShowHideMenu(bool show)
        {
            if (menuPanel != null)
                menuPanel.SetActive(show);
        }
    }
}