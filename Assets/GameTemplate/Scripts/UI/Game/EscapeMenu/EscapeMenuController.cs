using GameTemplate.Scripts.Systems.Input;
using UnityEngine;
using VContainer;

namespace GameTemplate.Scripts.UI.Game.EscapeMenu
{
    public class EscapeMenuController : MonoBehaviour
    {
        public EscapeMenuView view;
        private EscapeMenuPresenter presenter;
        
        private Controls _controls;

        [Inject]
        public void Construct(Controls controls)
        {
            _controls = controls;
            _controls.Enable();
            
            _controls.UI.Cancel.performed += ctx => presenter.ToggleMenu();
        }

        private void Awake()
        {
            presenter = new EscapeMenuPresenter(view);
        }
    }
}