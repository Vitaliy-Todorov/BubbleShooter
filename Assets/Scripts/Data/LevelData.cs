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

            MiddleOfWidth(out float widdleOfWidth);

            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                BallSpawnerData ball = new BallSpawnerData(0, new Vector2(x - widdleOfWidth, y));
                
                _stationaryBalls.Add(ball);
            }
            
            return _stationaryBalls;
        }

        private void MiddleOfWidth(out float widdleOfWidth)
        {
            if (Width % 2 == 0)
                widdleOfWidth = (float) Width / 2 - .5f;
            else
                widdleOfWidth = (float) Width / 2;
        }
    }
}