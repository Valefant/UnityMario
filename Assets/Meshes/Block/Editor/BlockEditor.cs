using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{
    Block block;

    int selectedIndex;
    string[] options = new string[] { "Brick", "Question Mark", "Exclamation Red", "Exclamation Green", "Exclamation Blue" };

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

        selectedIndex = EditorGUILayout.Popup("Blocktype", options.ToList<string>().IndexOf(block.blocktype), options);

        block.blocktype = options[selectedIndex];

        if (EditorGUI.EndChangeCheck())
        {
            block.CreateMesh();
        }
    }
}