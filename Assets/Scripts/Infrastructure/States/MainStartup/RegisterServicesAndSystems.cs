using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.System;
using Assets.Scripts.Infrastructure.System.InputSystem;
using Assets.Scripts.Logic;
using Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.States
{
    public class RegisterServicesAndSystems
    {
        private DependencyInjection _container = DependencyInjection.Container;
        private MainStartup _mainStartup;
        private GameStateMachine _gameStateMachine;

        public RegisterServicesAndSystems(GameStateMachine gameStateMachine, MainStartup mainStartup)
        {
            _gameStateMachine = gameStateMachine;
            _mainStartup = mainStartup;

            Enter();
        }

        private void Enter()
        {
            Registration();

            _mainStartup.UpdateEvent += Update;
            _mainStartup.FixedUpdateEvent += FixedUpdate;
        }

        public void Exit() { }

        private void Update()
        {
            _container.GetDependency<IInputSystem>().Update();
            _container.GetDependency<WindowManagementSystem>().Update();
        }

        private void FixedUpdate() { }

        private void Registration()
        {
            _container.RegisterDependency<IInputSystem>(new KeyboardAndMouseInputSystem());
            
            _container.RegisterDependency(new StaticDataService());
            _container.RegisterDependency(GeneralDataService());
            _container.RegisterDependency(SaveLoadService());

            _container.RegisterDependency(new AssetProvider());
            _container.RegisterDependency(WindowManagementSystem());
            _container.RegisterDependency(UIFactoryService());
            _container.RegisterDependency(GameFactoryService());
        }

        private GeneralDataService GeneralDataService()
        {
            StaticDataService staticDataService = _container.GetDependency<StaticDataService>();
            return new GeneralDataService(staticDataService);
        }

        private SaveLoadService SaveLoadService()
        {
            string filePath = Application.persistentDataPath;
            GeneralDataService generalDataService = _container.GetDependency<GeneralDataService>();
            
            return new SaveLoadService(_gameStateMachine, generalDataService, filePath);
        }

        private UIFactoryService UIFactoryService()
        {
            AssetProvider assetProvider = _container.GetDependency<AssetProvider>();
            WindowManagementSystem windowManagementSystem = _container.GetDependency<WindowManagementSystem>();
            StaticDataService staticDataService = _container.GetDependency<StaticDataService>();
            SaveLoadService saveLoadService = _container.GetDependency<SaveLoadService>();
            GeneralDataService generalDataService = _container.GetDependency<GeneralDataService>();
            IInputSystem inputSystem = _container.GetDependency<IInputSystem>();
            
            return new UIFactoryService(_gameStateMachine, assetProvider, staticDataService, windowManagementSystem, saveLoadService, generalDataService, inputSystem);
        }
        
        private GameFactoryService GameFactoryService()
        {
            AssetProvider assetProvider = _container.GetDependency<AssetProvider>();
            WindowManagementSystem windowManagementSystem = _container.GetDependency<WindowManagementSystem>();
            SaveLoadService saveLoadService = _container.GetDependency<SaveLoadService>();
            GeneralDataService generalDataService = _container.GetDependency<GeneralDataService>();
            UIFactoryService iuFactoryService = _container.GetDependency<UIFactoryService>();
            IInputSystem inputSystem = _container.GetDependency<IInputSystem>();
            
            return new GameFactoryService(assetProvider, generalDataService, iuFactoryService, inputSystem);
        }

        private WindowManagementSystem WindowManagementSystem()
        {
            IInputSystem inputSystem = _container.GetDependency<IInputSystem>();
            return new WindowManagementSystem(inputSystem);
        }
    }
}