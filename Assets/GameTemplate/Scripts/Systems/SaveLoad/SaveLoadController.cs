using UnityEngine;

namespace GameTemplate.Scripts.Systems.SaveLoad
{
    public class SaveLoadController : MonoBehaviour
    {
        public SaveLoadView view;
        private SaveLoadPresenter presenter;

        private void Awake()
        {
            presenter = new SaveLoadPresenter(view);
        }
    }
}