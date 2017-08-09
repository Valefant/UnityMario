using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Block : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Mesh mesh;

    public float size = 1f;
    public float height = 1f;
    public float width = 1f;
    public float depth = 1f;

    private Dictionary<Level, string> blockTypeToFilename = new Dictionary<Level, string>()
    {
        { Level.BRICK_BLOCK, "brick" },
        { Level.QUESTION_MARK_BLOCK, "question mark" },
        { Level.EXCLAMATION_RED_BLOCK, "exclamation red" },
        { Level.EXCLAMATION_GREEN_BLOCK, "exclamation green" },
        { Level.EXCLAMATION_BLUE_BLOCK, "exclamation blue" },
    };

    public String blocktype = "Brick";

    Vector3 P0, P1, P2, P3, P4, P5, P6, P7;

    public void Reset()
    {
        CreateMesh();
    }

    public void CreateMesh()
    {
        height *= size;
        width *= size;
        depth *= size;

        SetUpComponents("block");

        CreatePoints();

        Vector3[] vertices = new Vector3[24] 
        {
            // front vertices
            P0, P1, P2, P3,
            
            // back vertices
            P4, P5, P6, P7,
            
            // left vertices
            P0, P1, P6, P7,
            
            // right vertices
            P2, P3, P4, P5,
            
            // top vertices
            P1, P7, P3, P5,
            
            // bottom vertices
            P0, P6, P2, P4
        };

        int[] triangles = new int[36]
        {
            // front face
            0, 1, 2,
            2, 1, 3,

            // back face
            4, 5, 6,
            6, 5, 7,

            // left face
            10, 9, 8,
            10, 11, 9,

            // right face
            12, 13, 14,
            13, 15, 14,

            // top face
            16, 17, 18,
            18, 17, 19,

            // bottom face
            22, 21, 20,
            22, 23, 21
        };

        Vector2[] uvs = new Vector2[24]
        {
            // front uv
            new Vector2(0f, 0f), new Vector2(0f, height), new Vector2(width, 0f), new Vector2(width, height),

            // back uv
            new Vector2(0f, 0f), new Vector2(0f, height), new Vector2(width, 0f), new Vector2(width, height),

            // left uv
            new Vector2(depth, 0f), new Vector2(depth, height), new Vector2(0f, 0f), new Vector2(0f, height),

            // right uv
            new Vector2(0f, 0f), new Vector2(0f, height), new Vector2(depth, 0f), new Vector2(depth, height),

            // top uv
            new Vector2(0f, height), new Vector2(depth, height), new Vector2(0f, 0f), new Vector2(depth, 0f),

            // bottom uv
            new Vector2(0f, height), new Vector2(depth, height), new Vector2(0f, 0f), new Vector2(depth, 0f)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        meshFilter.mesh = mesh;

        Material[] materials = new Material[1] { new Material(Shader.Find("Diffuse")) };
        materials[0].mainTexture = Resources.Load(blocktype.ToLower()) as Texture;

        meshRenderer.materials = materials;
    }

    private void SetUpComponents(string meshname)
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = this.gameObject.AddComponent<MeshFilter>();

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();

        this.mesh = meshFilter.sharedMesh;

        if (this.mesh == null)
        {
            this.mesh = new Mesh();
            this.mesh.name = meshname;
        }
    }

    private void CreatePoints()
    {
        P0 = Vector3.zero;
        P1 = new Vector3(0f, height, 0f);
        P2 = new Vector3(width, 0f, 0f);
        P3 = new Vector3(width, height, 0f);

        P4 = new Vector3(width, 0f, depth);
        P5 = new Vector3(width, height, depth);
        P6 = new Vector3(0f, 0f, depth);
        P7 = new Vector3(0f, height, depth);
    }

    private void OnDestroy()
    {
        Debug.Log("Hallo");
    }

#if UNITY_EDITOR
    [MenuItem("GameObject/Mario/Block", false, 50)]
    public static Block CreateHouse()
    {
        GameObject gameObject = new GameObject("Block");
        Block block = gameObject.AddComponent<Block>();

        block.CreateMesh();

        return block;
    }
#endif
}
