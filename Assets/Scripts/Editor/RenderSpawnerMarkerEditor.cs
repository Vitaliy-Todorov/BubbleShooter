using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(StationaryBallSpawnMarker))]
    public class RenderSpawnerMarkerEditor : UnityEditor.Editor
    {
        private const string StaticGeneralDataPath = "Data/StaticGeneralData";
        private static StaticGeneralData _staticGeneralData;
        
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(StationaryBallSpawnMarker spawner, GizmoType gizmo)
        {
            if (_staticGeneralData != null)
            {
                Gizmos.color = (Color) _staticGeneralData.ColorsOfBalls[(int) spawner.Color];
                Gizmos.DrawSphere(spawner.transform.position, 0.5f);
            }
            else
                _staticGeneralData = Resources.Load<StaticGeneralData>(StaticGeneralDataPath);
        }
    }
}