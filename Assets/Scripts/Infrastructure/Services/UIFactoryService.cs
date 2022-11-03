using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.Infrastructure.System;
using Assets.Scripts.Infrastructure.System.InputSystem;
using Assets.Scripts.UI.Menu;
using Infrastructure.Services;
using UI.Menu;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public class UIFactoryService : IService
    {
        private GameStateMachine _gameStateMachine;
        private AssetProvider _assetProvider;
        private StaticDataService _staticDataService;
        private WindowManagementSystem _windowManagementSystem;
        private SaveLoadService _saveLoadService;
        private GeneralDataService _generalDataService;
        private IInputSystem _inputSystem;

        public UIFactoryService(GameStateMachine gameStateMachine,
            AssetProvider assetProvider,
            StaticDataService staticDataService,
            WindowManagementSystem windowManagementSystem,
            SaveLoadService saveLoadService,
            GeneralDataService generalDataService,
            IInputSystem inputSystem)
        {
            _gameStateMachine = gameStateMachine;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _windowManagementSystem = windowManagementSystem;
            _saveLoadService = saveLoadService;
            _generalDataService = generalDataService;
            _inputSystem = inputSystem;
        }

        public MainMenuGeneral CreateMainMenu()
        {
            GameObject asset = _assetProvider.Initializebl(AssetAddressAndNames.MainMenu);
            GameObject mainMenuGO = Object.Instantiate(asset);
            MainMenuGeneral mainMenu = mainMenuGO.GetComponentInChildren<MainMenuGeneral>();
            
            mainMenu.Construct(_gameStateMachine, _staticDataService);
            
            return mainMenu;
        }

        public GameMenuGeneral CreateGameMenu()
        {
            GameObject asset = _assetProvider.Initializebl(AssetAddressAndNames.GameMenu);
            GameObject gameMenuGO = Object.Instantiate(asset);
            GameMenuGeneral gameMenu = gameMenuGO.GetComponentInChildren<GameMenuGeneral>();

            gameMenu.Construct(_gameStateMachine, _generalDataService, _staticDataService, _inputSystem);

            _windowManagementSystem.GameMenu = gameMenuGO;
            // gameMenuGO.SetActive(false);
            return gameMenu;
        }

        public GameOverMenuGeneral CreateGameOverMenu()
        {
            Time.timeScale = 0;
            
            GameObject asset = _assetProvider.Initializebl(AssetAddressAndNames.GameOverMenu);
            GameObject gameOverMenuGeneralGO = Object.Instantiate(asset);
            GameOverMenuGeneral gameOverMenuGeneral = gameOverMenuGeneralGO.GetComponentInChildren<GameOverMenuGeneral>();

            gameOverMenuGeneral.Construct(_gameStateMachine);

            _windowManagementSystem.GameMenu = gameOverMenuGeneralGO;
            
            return gameOverMenuGeneral;
        }
    }
}