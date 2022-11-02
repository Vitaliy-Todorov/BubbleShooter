using System.Collections.Generic;
using Editor;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string NameScene;
        public Vector3 Gun;
        public List<BallSpawnerData> StationaryBalls;
        
        public float BottomOfGrid;
        public float TopOfGrid;
    }
}