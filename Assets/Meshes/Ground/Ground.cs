using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Ground : MonoBehaviour {
    #region Parameter
    public float Height = 5;
    public float Width = 15;
    public float Depth = 2;
    public float groundHeight = 0.8f;
    #endregion

    public Vector3 P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11;

    #region Mesh
    public Mesh mesh;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    #endregion

    #region Lists - vertices, triangles, uvs
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();
    #endregion

    public void CreateMesh()
    {
        Mesh();
        Clear();
        Positions();
        int[] triangle = Triangles();
        Vector3[] vertices = Vertices();

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.SetTriangles(triangle, 0);
        // uvs
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    private void Mesh()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null)
            meshFilter = this.gameObject.AddComponent<MeshFilter>();


        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();


        mesh = meshFilter.sharedMesh;

        if (mesh == null)
            mesh = new Mesh();
    }

    private void Clear()
    {
        vertices.Clear();
        uvs.Clear();
        triangles.Clear();
    }

    private void Positions()
    {
        // Width, Height and Depth: , Width, Height, Depth

        // Front
        P0 = MyExtensions.Vector3(0, 0, 0, Width, Height, Depth);
        P1 = MyExtensions.Vector3(0,1*groundHeight, 0, Width, Height, Depth);
        P2 = MyExtensions.Vector3(1,1*groundHeight, 0, Width, Height, Depth);
        P3 = MyExtensions.Vector3(1,0,0, Width, Height, Depth);

        // Back
        P4 = MyExtensions.Vector3(0,0,1, Width, Height, Depth);
        P5 = MyExtensions.Vector3(0,1*groundHeight,1, Width, Height, Depth);
        P6 = MyExtensions.Vector3(1,1*groundHeight,1, Width, Height, Depth);
        P7 = MyExtensions.Vector3(1,0,1, Width, Height, Depth);

        // Submesh
        P8 = MyExtensions.Vector3(0, 1, 0, Width, Height, Depth); // Submesh
        P9 = MyExtensions.Vector3(1, 1, 0, Width, Height, Depth); // Submesh
        P10 = MyExtensions.Vector3(0,1,1, Width, Height, Depth); // Submesh
        P11 = MyExtensions.Vector3(1,1,1, Width, Height, Depth); // Submesh

    }

    private int[] Triangles()
    {
        int[] triangle = new int[60];

        return triangle;
    }

    private Vector3[] Vertices()
    {
        Vector3[] vertices = new Vector3[12];
        vertices[0] = P0;
        vertices[1] = P1;
        vertices[2] = P2;
        vertices[3] = P3;
        vertices[4] = P4;
        vertices[5] = P5;
        vertices[6] = P6;
        vertices[7] = P7;
        vertices[8] = P8;
        vertices[9] = P9;
        vertices[10] = P10;
        vertices[11] = P11;

        return vertices;
    }

    private void UVS()
    {
        Vector3[] uvs = new Vector3[36];
    }

    private void TextureAndMaterial()
    {

    }

#if UNITY_EDITOR
    [MenuItem("GameObject/Primitives/Ground", false, 50)]
    public static Ground CreateGround()
    {
        GameObject gO = new GameObject("Ground");
        Ground ground = gO.AddComponent<Ground>();

        ground.CreateMesh();

        return ground;
    }
#endif
}
