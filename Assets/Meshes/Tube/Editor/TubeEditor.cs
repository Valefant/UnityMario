using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tube))]
public class TubeEditor : Editor
{
    Tube tube;

    void Awake()
    {
        tube = target as Tube;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUI.BeginChangeCheck();
        tube.height = EditorGUILayout.Slider("Height", tube.height, 0f, 10f);
        //tube.circleWidth = EditorGUILayout.Slider("Circle width", tube.circleWidth, 0f, 10f);
        //if (EditorGUI.EndChangeCheck())
        //{
        //    tube.CreateMesh();
        //}
    }
}
