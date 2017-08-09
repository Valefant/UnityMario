using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrid : MonoBehaviour
{
    public int depth = 1;
    public List<int> columnHeights = new List<int>() { 1, 2 };
    public List<GameObject> blocks;

    public void Reset()
    {
        foreach (GameObject block in blocks)
        {
            Destroy(block);
        }

        blocks = new List<GameObject>();

        CreateBlockGrid();
    }

    public void CreateBlockGrid()
    {
        int blocksCreated = 0;

        for (int d = 0; d < depth; d++)
        {
            for (int w = 0; w < columnHeights.Count; w++)
            {
                for (int h = 0; h < columnHeights[w]; h++)
                {
                    GameObject block = new GameObject();
                    block.AddComponent<Block>();

                    Transform transform = block.GetComponent<Transform>();
                    transform.position = new Vector3(w, h, d);

                    block.transform.parent = this.transform;
                    block.name = "Block " + blocksCreated;
                
                    blocks.Add(block);

                    blocksCreated++;
                }
            }
        }
    }
}