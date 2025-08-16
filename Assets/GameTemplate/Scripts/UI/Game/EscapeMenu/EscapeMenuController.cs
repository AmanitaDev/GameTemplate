using GameTemplate.Scripts.Systems.Input;
using UnityEngine;
using VContainer;

namespace GameTemplate.Scripts.UI.Game.EscapeMenu
{
    public class EscapeMenuController : MonoBehaviour
    {
        public EscapeMenuView view;
        private EscapeMenuMoel _moel;
        
        private Controls _controls;

        [Inject]
        public void Construct(Controls controls)
        {
            _controls = controls;
            _controls.Enable();
            
            _controls.UI.Cancel.performed += ctx => _moel.ToggleMenu();
        }

        private void Awake()
        {
            _moel = new EscapeMenuMoel(view);
        }
    }
}