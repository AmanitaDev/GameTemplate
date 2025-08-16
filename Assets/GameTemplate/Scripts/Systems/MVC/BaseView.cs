using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts.Systems.MVC
{
    public class BaseView : MonoBehaviour, IView
    {
        public void BindButton(Button button, UnityEngine.Events.UnityAction action)
        {
            if (button != null && action != null)
                button.onClick.AddListener(action);
        }
    }
}