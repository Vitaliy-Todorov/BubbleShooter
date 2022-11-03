using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.System.InputSystem;
using Component;
using Component.BallsGrid;
using Data;
using UnityEngine;

namespace Infrastructure.Services
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
            GameObject assetGun = _assetProvider.Initializebl(AssetAddressAndNames.Gun);
            GameObject gunGO = Object.Instantiate(assetGun, position, Quaternion.identity);
            
            ShootingBalls gun = gunGO.GetComponentInChildren<ShootingBalls>();
            gun.Construct(_generalDataService, _inputSystem, ballsGridMove);
            
            TurnGan turnGan = gunGO.GetComponentInChildren<TurnGan>();
            turnGan.Construct(_inputSystem);
            
            return gun;
        }

        #region FrameFromWalls
        
        public void CreateFrameFromWalls(Vector2 lowerLeftCorner, Vector2 upperRightCorner)
        {
            GameObject assetWall = _assetProvider.Initializebl(AssetAddressAndNames.Wall);
            Vector2 sizeOfFrame = upperRightCorner - lowerLeftCorner;

            CreateWall(assetWall, 
                new Vector2(0, lowerLeftCorner.y), 
                new Vector2(sizeOfFrame.x, 1));
            CreateWall(assetWall, 
                new Vector2(lowerLeftCorner.x, 0), 
                new Vector2(1, sizeOfFrame.y));
            CreateWall(assetWall, 
                new Vector2(upperRightCorner.x, 0), 
                new Vector2(1, sizeOfFrame.y));

            GameObject wallForSpawnTheBallsGridPattern = CreateWall(assetWall, 
                new Vector2(0, upperRightCorner.y), 
                new Vector2(sizeOfFrame.x, 1));

            wallForSpawnTheBallsGridPattern
                .GetComponent<BoxCollider2D>()
                .isTrigger = true;
            
            wallForSpawnTheBallsGridPattern
                .AddComponent<SpawnTheBallsGridPattern>()
                .Construct(this);
        }

        private GameObject CreateWall(GameObject assetWall, Vector2 position, Vector2 sizeOfFrame)
        {
            Vector3 scal = (Vector3) sizeOfFrame + new Vector3(0,0,1);
            GameObject wallGO = Object.Instantiate(assetWall, position, Quaternion.identity);
            wallGO.transform.localScale = scal;

            return wallGO;
        }

        #endregion

        #region BallsGrid
        
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

            return baallsGridGO;
        }

        public GameObject CreateBallsGridPattern(BallsGridPattern ballsGridPattern) =>
            CreateBallsGridPattern(ballsGridPattern.BallsSpawnerData,
                ballsGridPattern.BallsGridMove,
                ballsGridPattern.StartPositionGridBalls,
                ballsGridPattern.GridHeight);

        private GameObject CreateBallsGridPattern(List<BallSpawnerData> ballsSpawnerData, BallsGridMove ballsGridMove, Vector2 startPositionGridBalls, float gridHeight)
        {
            GameObject assetBallsGridPattern = _assetProvider.Initializebl(AssetAddressAndNames.BallsGridPattern);
            
            GameObject baallsGridPatternGO = Object.Instantiate(assetBallsGridPattern, startPositionGridBalls, Quaternion.identity, ballsGridMove.transform);
            baallsGridPatternGO.GetComponent<BallsGridPattern>()
                .Construct(ballsSpawnerData, ballsGridMove, startPositionGridBalls, gridHeight);
            
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
        
        #endregion
    }
}