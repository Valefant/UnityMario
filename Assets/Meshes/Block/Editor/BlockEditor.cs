using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{
    Block block;

    void Awake()
    {
        block = target as Block;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        block.size = EditorGUILayout.Slider("Size", block.size, 0f, 100f);
        block.height = EditorGUILayout.Slider("Height", block.height, 0f, 100f);
        block.width = EditorGUILayout.Slider("Width", block.width, 0f, 100f);
        block.depth = EditorGUILayout.Slider("Depth", block.depth, 0f, 100f);

        int selectedIndex = 0;
        string[] options = new string[] { "Brick", "Question Mark", "Exclamation Red", "Exclamation Green", "Exclamation Blue" };
        selectedIndex = EditorGUILayout.Popup("Blocktype", selectedIndex, options);

        block.blocktype = options[selectedIndex];

        if (EditorGUI.EndChangeCheck())
        {
            block.CreateMesh();
        }
    }
}