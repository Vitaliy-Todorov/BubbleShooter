using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.System.InputSystem;
using Component;
using Component.BallsGrid;
using Data;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Assets.Scripts.Infrastructure.Services
{
    public class GameFactoryService : IService
    {
        private AssetProvider _assetProvider;
        private GeneralDataService _generalDataService;
        private IInputSystem _inputSystem;
        
        private float _diameterOfBall = 1;

        public GameFactoryService(AssetProvider assetProvider,
            GeneralDataService generalDataService,
            IInputSystem inputSystem)
        {
            _assetProvider = assetProvider;
            _generalDataService = generalDataService;
            _inputSystem = inputSystem;
        }

        public ShootingBalls CreateGun(Vector2 position, BallsGridMove ballsGridMove)
        {
            GameObject asset = _assetProvider.Initializebl(AssetAddressAndNames.Gun);
            GameObject gunGO = Object.Instantiate(asset, position, Quaternion.identity);
            
            ShootingBalls gun = gunGO.GetComponentInChildren<ShootingBalls>();
            gun.Construct(_generalDataService, _inputSystem, ballsGridMove);
            
            TurnGan turnGan = gunGO.GetComponentInChildren<TurnGan>();
            turnGan.Construct(_inputSystem);
            
            return gun;
        }

        public GameObject CreateBallsGrid(List<BallSpawnerData> ballsSpawnerData, float topOfGrid, float bottomOfGrid)
        {
            GameObject assetBallsGrid = _assetProvider.Initializebl(AssetAddressAndNames.BallsGrid);
            GameObject baallsGridGO = Object.Instantiate(assetBallsGrid);

            BallsGridMove ballsGridMove = baallsGridGO.GetComponent<BallsGridMove>();
            ballsGridMove.Construct(new Vector3(0, _diameterOfBall, 0));

            float gridHeight = topOfGrid - bottomOfGrid + _diameterOfBall;
            #region CreateBallsGridPattern
            Vector2 startPositionGridBalls = new Vector2(0, (topOfGrid + bottomOfGrid) / 2);
            GameObject assetBallsGridPattern = CreateBallsGridPattern(ballsSpawnerData, ballsGridMove, startPositionGridBalls, gridHeight);
            #endregion
            
            assetBallsGridPattern.transform.SetParent(baallsGridGO.transform);

            return baallsGridGO;
        }

        private GameObject CreateBallsGridPattern(List<BallSpawnerData> ballsSpawnerData, BallsGridMove ballsGridMove, Vector2 startPositionGridBalls, float gridHeight)
        {
            GameObject assetBallsGridPattern = _assetProvider.Initializebl(AssetAddressAndNames.BallsGridPattern);
            
            GameObject baallsGridPatternGO = Object.Instantiate(assetBallsGridPattern, startPositionGridBalls, Quaternion.identity);
            baallsGridPatternGO.transform.localScale = new Vector3(1, gridHeight, 1);

            FillBallsGridPattern(ballsSpawnerData, ballsGridMove, ref baallsGridPatternGO);
            

            return baallsGridPatternGO;
        }

        private void FillBallsGridPattern(List<BallSpawnerData> ballsSpawnerData, BallsGridMove ballsGridMove, ref GameObject baallsGridPatternGO)
        {
            foreach (BallSpawnerData ballSpawnerData in ballsSpawnerData)
                CreatStationaryBall(ballsGridMove, ballSpawnerData)
                    .transform.SetParent(baallsGridPatternGO.transform);
        }

        private GameObject CreatStationaryBall(BallsGridMove ballsGridMove, BallSpawnerData ballSpawnerData)
        {
            GameObject assetStationaryBall = _assetProvider.Initializebl(AssetAddressAndNames.StationaryBall);
            GameObject stationaryBallGO = Object.Instantiate(assetStationaryBall, ballSpawnerData.Position, Quaternion.identity);
            
            Ball ball = stationaryBallGO.GetComponentInChildren<Ball>();
            Color color = _generalDataService.GeneralData.ColorsOfBalls[(int) ballSpawnerData.Color];
            ball.Construct(color);

            return stationaryBallGO;
        }
    }
}