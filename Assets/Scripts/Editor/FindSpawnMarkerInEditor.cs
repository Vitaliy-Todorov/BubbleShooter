using System.Linq;
using Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(LevelData))]
    public class FindSpawnMarkerInEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelData levelData = (LevelData) target;

            if (GUILayout.Button("Collect"))
            {
                levelData.NameScene = SceneManager.GetActiveScene().name;
                
                levelData.Gun = 
                    FindObjectOfType<GunSpawnMarker>()
                    .transform.position;
                
                levelData.StationaryBalls = 
                    FindObjectsOfType<StationaryBallSpawnMarker>()
                        .Select(spawnMarker => new BallSpawnerData(spawnMarker.Color, spawnMarker.transform.position))
                        .ToList();

                FindTopAndBottomOfGrid(levelData);
            }
        }

        private static void FindTopAndBottomOfGrid(LevelData levelData)
        {
            if(levelData.StationaryBalls.Count == 0)
                return;
            
            levelData.TopOfGrid = levelData.StationaryBalls[0].Position.y;
            levelData.BottomOfGrid = levelData.StationaryBalls[0].Position.y;

            foreach (BallSpawnerData ballSpawnerData in levelData.StationaryBalls)
            {
                if (levelData.TopOfGrid < ballSpawnerData.Position.y)
                    levelData.TopOfGrid = ballSpawnerData.Position.y;
                
                if (levelData.BottomOfGrid > ballSpawnerData.Position.y)
                    levelData.BottomOfGrid = ballSpawnerData.Position.y;
            }
        }
    }
}