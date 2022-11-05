using System;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.Infrastructure.System.InputSystem;
using Assets.Scripts.UI.Menu;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class GameMenuGeneral : MonoBehaviour
    {
        [SerializeField] 
        private GameMenu _gameMenu;
        [SerializeField] 
        private LevelMenu _levelMenu;
        [SerializeField] private GameObject _background ;
        
        [SerializeField] private CallingMainMenu _mainMenuButton;
        
        public bool _activeMenu;
        private IInputSystem _inputSystem;

        public void Construct(GameStateMachine gameStateMachine,
            GeneralDataService generalDataService,
            StaticDataService staticDataService,
            IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _gameMenu.Construct(gameStateMachine, generalDataService);
            
            _levelMenu.Construct(gameStateMachine, staticDataService);
            _mainMenuButton.Construct(this);
        }
        
        private void OnDestroy()
        {
            _inputSystem.InstantlyUnlock();
            Time.timeScale = 1;
        }

        public void EnableAndDisableMenu()
        {
            if (_activeMenu)
                DisableMenu();
            else
                EnableMenu();
        }

        private void EnableMenu()
        {
            _inputSystem.Block();
            _activeMenu = true;
            Time.timeScale = 0;
            _gameMenu.gameObject.SetActive(true);
            _background.SetActive(true);
        }

        private void DisableMenu()
        {
            _inputSystem.UnlockAfterUp();
            _activeMenu = false;
            _gameMenu.gameObject.SetActive(false);
            _background.SetActive(false);
            _levelMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}