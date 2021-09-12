using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor {
    private Waypoint Waypoint => target as Waypoint;

    private void OnSceneGUI() {
        Handles.color = Color.green;

        for (int index = 0; index < Waypoint.Points.Length; index++) {
            EditorGUI.BeginChangeCheck();

            //Create Handles
            Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[index];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            //Create Text
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.yellow;
            Vector3 textAlignment = Vector3.down * 0.5f + Vector3.right * 0.5f;
            Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[index] + textAlignment, $"{index + 1}", textStyle);

            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(target, "Free Move Handle");
                Waypoint.Points[index] = newWaypointPoint - Waypoint.CurrentPosition;
            }
        }
    }
}