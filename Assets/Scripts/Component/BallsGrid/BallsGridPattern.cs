using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Component.BallsGrid
{
    public class BallsGridPattern : MonoBehaviour
    {
        public List<BallSpawnerData> BallsSpawnerData;
        public BallsGridMove BallsGridMove;
        public Vector2 StartPositionGridBalls;
        public float GridHeight;
        public bool Random;
        
        public void Construct(List<BallSpawnerData> ballsSpawnerData, BallsGridMove ballsGridMove, Vector2 startPositionGridBalls, float gridHeight, bool random)
        {
            BallsSpawnerData = ballsSpawnerData;
            BallsGridMove = ballsGridMove;
            StartPositionGridBalls = startPositionGridBalls;
            GridHeight = gridHeight;
            Random = random;
        }
    }
}