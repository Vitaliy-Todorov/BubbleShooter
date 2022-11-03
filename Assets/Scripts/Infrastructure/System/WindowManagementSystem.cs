using Assets.Scripts.Infrastructure.System.InputSystem;
using Assets.Scripts.UI.Menu;
using UI.Menu;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.System
{
    public class WindowManagementSystem 
    {
        private IInputSystem _inputSystem;

        private GameObject _gameMenu;

        public GameObject GameMenu
        {
            get => _gameMenu;
            set => _gameMenu = value;
        }

        public WindowManagementSystem(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }

        public void Update()
        {
            if(_inputSystem.ESC)
                _gameMenu?.GetComponent<GameMenuGeneral>().EnableAndDisableMenu();
        }
    }
}