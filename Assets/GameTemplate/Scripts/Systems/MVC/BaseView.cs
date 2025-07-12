using UnityEngine;

namespace GameTemplate.Scripts.Systems.MVC
{
    public abstract class BaseView : MonoBehaviour, IMVCView
    {
        [SerializeField] private bool isOpenByDefault = true;
        public bool IsOpenByDefault => isOpenByDefault;

        public virtual void Show()
        {
            if (!isOpenByDefault) return;
            
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}