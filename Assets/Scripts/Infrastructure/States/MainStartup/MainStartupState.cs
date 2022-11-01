using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Logic;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure.States
{
    public class MainStartupState : IState
    {
        private GameStateMachine _gameStateMachine;
        private readonly GeneralDataService _generalDataService;
        private SaveLoadService _saveLoadService;

        public MainStartupState(GameStateMachine gameStateMachine, MainStartup mainStartup)
        {
            _gameStateMachine = gameStateMachine;

            new RegisterServicesAndSystems(gameStateMachine, mainStartup);

            _saveLoadService = DependencyInjection.Container.GetDependency<SaveLoadService>();
        }

        public void Enter()
        {
            _saveLoadService.LoadGeneralPlayerDataToFile();

            string activeScene = SceneManager.GetActiveScene().name;
            
            if(activeScene == AssetAddressAndNames.InitialScene)
                _gameStateMachine.Enter<LoadLeveState, string>(AssetAddressAndNames.MainMenuScene);
            else
                _gameStateMachine.Enter<LoadLeveState, string>(activeScene);
        }
        
        public void Exit() { }
    }
}