using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Component;
using Component.BallsGrid;
using Data;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure.States
{
    public class LoadLeveState : IPlayLoadState<string>
    {
        private GameStateMachine _gameStateMachine;
        private GeneralDataService _generalDataService;
        private StaticDataService _staticDataService;
        private SaveLoadService _saveLoadService;
        private GameFactoryService _gameFactoryService;
        private UIFactoryService _uiFactoryService;

        private SceneLoad _sceneLoad;

        public LoadLeveState(GameStateMachine gameStateMachine,
            GeneralDataService generalDataService,
            StaticDataService staticDataService,
            GameFactoryService factoryService,
            UIFactoryService uiFactoryService,
            SceneLoad sceneLoad)
        {
            _gameStateMachine = gameStateMachine;
            _generalDataService = generalDataService;
            _staticDataService = staticDataService;
            _gameFactoryService = factoryService;
            _uiFactoryService = uiFactoryService;
            
            _sceneLoad = sceneLoad;
        }


        public void Enter(string sceneName)
        {
            _sceneLoad.Load(sceneName, OnLoader);
            /*if(SceneManager.GetActiveScene().name != sceneName)
                _sceneLoad.Load(sceneName, OnLoader);
            else
                OnLoader();*/
        }

        public void Exit() { }

        private void OnLoader()
        {
            _generalDataService.PlayerProgressData.SceneName = SceneManager.GetActiveScene().name; 
            
            LoadingStateOfSpecificScene();
            InitGameWorld();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void LoadingStateOfSpecificScene()
        {
            if (SceneManager.GetActiveScene().name == AssetAddressAndNames.MainMenuScene)
            {
                _generalDataService.PlayerProgressData = new PlayerProgressData();
                _uiFactoryService.CreateMainMenu();
            }
            else
                _uiFactoryService.CreateGameMenu();
        }

        private void InitGameWorld()
        {
            string nameScene = SceneManager.GetActiveScene().name;
            LevelData levelData = _staticDataService.GetLevelData(nameScene);

            if(levelData == null)
                return;

            BallsGridMove ballsGridMove = _gameFactoryService
                .CreateBallsGrid(levelData)
                .GetComponent<BallsGridMove>();
            
            _gameFactoryService.CreateGun(levelData.Gun, ballsGridMove);
            
            _gameFactoryService.CreateFrameFromWalls(
                _generalDataService.GeneralData.lowerLeftCorner,
                _generalDataService.GeneralData.upperRightCorner,
                levelData.Gun
                );
        }
    }
}