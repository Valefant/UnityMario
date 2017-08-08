using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ground))]
public class GroundEditor : Editor
{
    Ground ground;

    void Awake()
    {
        ground = target as Ground;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        #region Parameters
        ground.Width = EditorGUILayout.Slider("Width", ground.Width, 1f, 100f);
        ground.Height = EditorGUILayout.Slider("Height", ground.Height, 1f, 100f);
        ground.Depth = EditorGUILayout.Slider("Depth", ground.Depth, 1f, 100f);
        #endregion // Parameters

        if (EditorGUI.EndChangeCheck())
            ground.CreateMesh();
    }
}
