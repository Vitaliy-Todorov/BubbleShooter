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
        private UIFactoryService _uiFactoryService;
        private IInputSystem _inputSystem;

        private float _diameterOfBall = 1;

        public GameFactoryService(AssetProvider assetProvider,
            GeneralDataService generalDataService,
            UIFactoryService uiFactoryService,
            IInputSystem inputSystem)
        {
            _uiFactoryService = uiFactoryService;
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
        
        public void CreateFrameFromWalls(Vector2 lowerLeftCorner, Vector2 upperRightCorner, Vector2 gunPosition)
        {
            GameObject assetWall = _assetProvider.Initializebl(AssetAddressAndNames.Wall);
            GameObject assetTriggerWall = _assetProvider.Initializebl(AssetAddressAndNames.TriggerWall);
            Vector2 sizeOfFrame = upperRightCorner - lowerLeftCorner;

            CreateWall(assetWall, 
                new Vector2(lowerLeftCorner.x, 0), 
                new Vector2(1, sizeOfFrame.y));
            CreateWall(assetWall, 
                new Vector2(0, lowerLeftCorner.y), 
                new Vector2(sizeOfFrame.x, 1));
            CreateWall(assetWall, 
                new Vector2(upperRightCorner.x, 0), 
                new Vector2(1, sizeOfFrame.y));
            CreateWall(assetWall, 
                new Vector2(0, upperRightCorner.y), 
                new Vector2(sizeOfFrame.x, 1));

            SpawnTheBallsGridPattern(upperRightCorner, assetTriggerWall, sizeOfFrame);

            TriggerForGameOver(gunPosition, assetTriggerWall, sizeOfFrame);
        }

        private void SpawnTheBallsGridPattern(Vector2 upperRightCorner, GameObject assetTriggerWall, Vector2 sizeOfFrame)
        {
            GameObject spawnTheBallsGridPattern = CreateWall(assetTriggerWall,
                new Vector2(0, upperRightCorner.y),
                new Vector2(sizeOfFrame.x, 1));

            spawnTheBallsGridPattern
                .AddComponent<SpawnTheBallsGridPattern>()
                .Construct(this);
        }

        private void TriggerForGameOver(Vector2 gunPosition, GameObject assetTriggerWall, Vector2 sizeOfFrame)
        {
            GameObject triggerForGameOver = CreateWall(assetTriggerWall,
                new Vector2(0, gunPosition.y),
                new Vector2(sizeOfFrame.x, 1));
            
            triggerForGameOver
                .AddComponent<TriggerForGameOver>()
                .Construct(_uiFactoryService);
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
        
        public BallsGridMove CreateBallsGrid(LevelData levelData)
        {
            TopAndBottomOfGrid(levelData, out float topOfGrid, out float bottomOfGrid); 
            float upperWall = _generalDataService.GeneralData.upperRightCorner.y;
            
            BallsGridMove ballsGridMove = 
                CreateBallsGrid(levelData.StationaryBalls, topOfGrid, bottomOfGrid, upperWall, levelData.Random)
                    .GetComponent<BallsGridMove>();
                
            return ballsGridMove;
        }

        private void TopAndBottomOfGrid(LevelData levelData, out float topOfGrid, out float bottomOfGrid)
        {
            if (levelData.Random)
            {
                topOfGrid = levelData.Height;
                bottomOfGrid = 0;
            }
            else
            {
                topOfGrid = levelData.TopOfGrid + _diameterOfBall/2;
                bottomOfGrid = levelData.BottomOfGrid - _diameterOfBall/2;
            }
        }

        private GameObject CreateBallsGrid(List<BallSpawnerData> ballsSpawnerData, float topOfGrid, float bottomOfGrid, float upperWall, bool random)
        {
            GameObject assetBallsGrid = _assetProvider.Initializebl(AssetAddressAndNames.BallsGrid);
            GameObject baallsGridGO = Object.Instantiate(assetBallsGrid);

            BallsGridMove ballsGridMove = baallsGridGO.GetComponent<BallsGridMove>();
            ballsGridMove.Construct(new Vector3(0, _diameterOfBall, 0));

            float gridHeight = topOfGrid - bottomOfGrid;
            #region CreateBallsGridPattern
            Vector2 startPositionGridBalls = new Vector2(0, (topOfGrid + bottomOfGrid) / 2);
            GameObject assetBallsGridPattern = CreateBallsGridPattern(ballsSpawnerData, ballsGridMove, startPositionGridBalls, gridHeight, random);
            #endregion

            // Два шара до верхней граници 
            assetBallsGridPattern.transform.position = new Vector2(0, upperWall + gridHeight/2 - 2 * _diameterOfBall);
                
            return baallsGridGO;
        }

        public GameObject CreateBallsGridPattern(BallsGridPattern ballsGridPattern) =>
            CreateBallsGridPattern(ballsGridPattern.BallsSpawnerData,
                ballsGridPattern.BallsGridMove,
                ballsGridPattern.StartPositionGridBalls,
                ballsGridPattern.GridHeight,
                ballsGridPattern.Random);

        private GameObject CreateBallsGridPattern(List<BallSpawnerData> ballsSpawnerData,
            BallsGridMove ballsGridMove,
            Vector2 startPositionGridBalls,
            float gridHeight,
            bool random)
        {
            GameObject assetBallsGridPattern = _assetProvider.Initializebl(AssetAddressAndNames.BallsGridPattern);
            
            GameObject baallsGridPatternGO = Object.Instantiate(assetBallsGridPattern, startPositionGridBalls, Quaternion.identity, ballsGridMove.transform);
            baallsGridPatternGO.GetComponent<BallsGridPattern>()
                .Construct(ballsSpawnerData, ballsGridMove, startPositionGridBalls, gridHeight, random);
            
            baallsGridPatternGO.transform.localScale = new Vector3(1, gridHeight, 1);

            FillBallsGridPattern(ballsSpawnerData, ref baallsGridPatternGO, random);
            
            return baallsGridPatternGO;
        }

        private void FillBallsGridPattern(List<BallSpawnerData> ballsSpawnerData, ref GameObject baallsGridPatternGO, bool random)
        {
            foreach (BallSpawnerData ballSpawnerData in ballsSpawnerData)
                CreatStationaryBall(ballSpawnerData, random)
                    .transform.SetParent(baallsGridPatternGO.transform);
        }

        private GameObject CreatStationaryBall(BallSpawnerData ballSpawnerData, bool random)
        {
            GameObject assetStationaryBall = _assetProvider.Initializebl(AssetAddressAndNames.StationaryBall);
            GameObject stationaryBallGO = Object.Instantiate(assetStationaryBall, ballSpawnerData.Position, Quaternion.identity);
            
            Ball ball = stationaryBallGO.GetComponentInChildren<Ball>();
            Color color = GetColor(ballSpawnerData, random);
            ball.Construct(color);

            return stationaryBallGO;
        }

        private Color GetColor(BallSpawnerData ballSpawnerData, bool random)
        {
            List<Color> colorsOfBalls = _generalDataService.GeneralData.ColorsOfBalls;
            if (random)
            {
                int randomIndex = Random.Range(0, colorsOfBalls.Count);
                return colorsOfBalls[randomIndex];
            }
            else
                return colorsOfBalls[(int) ballSpawnerData.Color];
        }

        #endregion
    }
}