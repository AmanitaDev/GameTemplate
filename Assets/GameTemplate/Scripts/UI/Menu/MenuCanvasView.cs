using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts.UI.Menu
{
    public class MenuCanvasView : MonoBehaviour
    {
        public Button ContinueButton;
        public Button PlayButton;
        public Button SettingsButton;
        public Button QuitButton;
        
        public GameObject ConfirmPanel;
        public Button ConfirmButton;
        public Button CancelButton;

        public void ChangeConfirmPanel(bool value)
        {
            ConfirmPanel.SetActive(value);
        }
        
        public void ChangeContinueButton(bool value)
        {
            ContinueButton.interactable = value;
        }
    }
}