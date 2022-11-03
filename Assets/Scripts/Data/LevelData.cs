using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string NameScene;
        public string NameLevel;
        public Vector3 Gun;
        public List<BallSpawnerData> _stationaryBalls;

        public bool Random;
        
        public float BottomOfGrid;
        public float TopOfGrid;

        public int Height;
        public int Width;

        public List<BallSpawnerData> StationaryBalls
        {
            get
            {
                if (Random && _stationaryBalls?.Count < Height * Width) 
                    _stationaryBalls = CreatBallsGrid();
                
                return _stationaryBalls;
            }
        }

        private List<BallSpawnerData> CreatBallsGrid()
        {
            _stationaryBalls = new List<BallSpawnerData>();
            
            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                BallSpawnerData ball = new BallSpawnerData(0, new Vector2(x - Width/2, y));
                
                _stationaryBalls.Add(ball);
            }
            
            return _stationaryBalls;
        }
    }
}