using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Star))]
public class StarEditor : Editor
{
    Star star;    

    void Awake()
    {
        star = target as Star;
    }

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		#region Parameters
		star.size = EditorGUILayout.Slider("Size", star.size, 1f, 100f);
		star.depth = EditorGUILayout.Slider("Depth", star.depth, 1f, 100f);
		#endregion // Parameters

		EditorGUILayout.BeginVertical("box");
		GUILayout.Label("Maintexture");
		star.mainTexture =
			EditorGUILayout.ObjectField(
				"MainTexture",
				star.mainTexture,
				typeof(Texture2D),
				true) as Texture2D;

		EditorGUILayout.EndVertical();

		if (EditorGUI.EndChangeCheck())
			star.CreateMesh();
	}
}
