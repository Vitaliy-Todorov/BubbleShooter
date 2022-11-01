using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class BallSpawnerData
    {
        public EColorBalls Color;
        public Vector2 Position;

        public BallSpawnerData(EColorBalls color, Vector2 position)
        {
            Color = color;
            Position = position;
        }
    }
}