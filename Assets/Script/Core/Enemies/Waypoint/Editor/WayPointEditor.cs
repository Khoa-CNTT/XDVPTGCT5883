using UnityEditor;
using UnityEngine;

namespace dang
{
    [CustomEditor(typeof(Waypoint))]
    public class WayPointEditor : Editor
    {
        Waypoint Waypoint => target as Waypoint;

        [System.Obsolete]
        private void OnSceneGUI()
        {

            Handles.color = Color.yellow;
            for (int i = 0; i < Waypoint.Points.Length; i++)
            {
                EditorGUI.BeginChangeCheck();

                //create Handles
                Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
                Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

                // create text
                GUIStyle textStyle = new GUIStyle();
                textStyle.fontStyle = FontStyle.Bold;
                textStyle.fontSize = 16;
                textStyle.normal.textColor = Color.yellow;

                Vector3 textAligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
                // Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAligment, $"Waypoint {i + 1}", textStyle);
                Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAligment, $"{i + 1}", textStyle);


                EditorGUI.EndChangeCheck();

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Free Move Handle");

                    // Round the new waypoint point to the nearest 0.5
                    Vector3 roundedPoint = new Vector3(
                        Mathf.Round((newWaypointPoint.x - Waypoint.CurrentPosition.x) * 2) / 2,
                        Mathf.Round((newWaypointPoint.y - Waypoint.CurrentPosition.y) * 2) / 2,
                        Mathf.Round((newWaypointPoint.z - Waypoint.CurrentPosition.z) * 2) / 2
                    );

                    Waypoint.Points[i] = roundedPoint;
                }
            }
        }
    }
}
