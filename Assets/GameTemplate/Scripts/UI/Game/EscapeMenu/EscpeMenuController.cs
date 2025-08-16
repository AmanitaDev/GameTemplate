using UnityEngine;

namespace GameTemplate.Scripts.UI.Game.EscapeMenu
{
    public class EscpeMenuController : MonoBehaviour
    {
        public EscapeMenuView view;
        private EscapeMenuPresenter presenter;

        private void Awake()
        {
            presenter = new EscapeMenuPresenter(view);
        }

        private void Update()
        {
            // Toggle menu with Escape key
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                presenter.ToggleMenu();
            }
        }
    }
}