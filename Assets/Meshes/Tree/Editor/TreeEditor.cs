using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Castle))]
public class TreeEditor : Editor
{
    Tree tree;

    void Awake()
    {
		tree = target as Tree;
    }

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		#region Parameters
		tree.size = EditorGUILayout.Slider("Size", tree.size, 1f, 100f);
		tree.diameter = EditorGUILayout.Slider("Diameter", tree.diameter, 1f, 100f);
		#endregion // Parameters

		EditorGUILayout.BeginVertical("box");
		GUILayout.Label("Maintexture");
		tree.mainTexture =
			EditorGUILayout.ObjectField(
				"MainTexture",
				tree.mainTexture,
				typeof(Texture2D),
				true) as Texture2D;

		EditorGUILayout.EndVertical();

		if (EditorGUI.EndChangeCheck())
			tree.CreateMesh();
	}
}
