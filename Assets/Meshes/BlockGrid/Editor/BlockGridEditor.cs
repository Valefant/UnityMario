using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockGrid))]
public class BlockGridEditor : Editor
{
    BlockGrid blockGrid;

    void Awake()
    {
        blockGrid = target as BlockGrid;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        blockGrid.depth = EditorGUILayout.IntSlider("Depth", blockGrid.depth, 1, 10);

        if (EditorGUI.EndChangeCheck())
        {
            foreach (GameObject block in blockGrid.blocks)
            {
                DestroyImmediate(block);
            }

            blockGrid.blocks.Clear();

            blockGrid.CreateBlockGrid();
        }
    }
}
