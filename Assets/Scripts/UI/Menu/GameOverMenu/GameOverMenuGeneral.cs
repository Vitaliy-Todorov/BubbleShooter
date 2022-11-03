using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.UI.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class GameOverMenuGeneral : MonoBehaviour
    {
        [SerializeField] private Button _mainMenu;
        [SerializeField] private Button _exet;
        
        private GameStateMachine _gameStateMachine;
        
        public void Construct(GameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        void Start()
        {
            _mainMenu.onClick.AddListener(MainMenu);
            _exet.onClick.AddListener(Exet);
        }

        private void MainMenu() => 
            _gameStateMachine.Enter<LoadLeveState, string>(AssetAddressAndNames.MainMenuScene);

        private void Exet() => 
            Application.Quit();
    }
}
