using UnityEngine.UI;

namespace GameTemplate.Scripts.Systems.MVC
{
    public interface IView
    {
        void BindButton(Button button, UnityEngine.Events.UnityAction action);
    }
}