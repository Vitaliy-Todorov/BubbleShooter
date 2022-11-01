using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.System.InputSystem;
using Component;
using Data;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public class GameFactoryService : IService
    {
        private AssetProvider _assetProvider;
        private GeneralDataService _generalDataService;
        private IInputSystem _inputSystem;

        public GameFactoryService(AssetProvider assetProvider,
            GeneralDataService generalDataService,
            IInputSystem inputSystem)
        {
            _assetProvider = assetProvider;
            _generalDataService = generalDataService;
            _inputSystem = inputSystem;
        }

        public ShootingBalls CreateGun(Vector2 position)
        {
            GameObject asset = _assetProvider.Initializebl(AssetAddressAndNames.Gun);
            GameObject gunGO = Object.Instantiate(asset, position, Quaternion.identity);
            
            ShootingBalls gun = gunGO.GetComponentInChildren<ShootingBalls>();
            gun.Construct(_generalDataService, _inputSystem);
            
            return gun;
        }

        public void CreateBallsGrid(string levelName, List<BallSpawnerData> ballsSpawnerData)
        {
            GameObject asset = _assetProvider.Initializebl(AssetAddressAndNames.StationaryBall);

            foreach (BallSpawnerData ballSpawnerData in ballsSpawnerData)
            {
                GameObject stationaryBall = Object.Instantiate(asset, ballSpawnerData.Position, Quaternion.identity);
                Ball gun = stationaryBall.GetComponentInChildren<Ball>();
                Color color = _generalDataService.GeneralData.ColorsOfBalls[(int) ballSpawnerData.Color];
                gun.Construct(color);
            }
        }
    }
}