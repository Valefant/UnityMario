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

    // Arrays für die Submeshes
    // TODO: ein Array pro submesh
    int[] mainPartTriangle = new int[36];
    int[] topPartTriangle = new int[36];
    #endregion

    public void Reset()
    {
        CreateMesh();
    }

    public void CreateMesh()
    {
        Mesh();
        Clear();
        Positions();
        
        Vector3[] vertices = Vertices();

        mesh.Clear();
        mesh.vertices = vertices;
        Triangles();
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

    private void Triangles()
    {
        #region main part
        // Front
        mainPartTriangle[0] = 0;
        mainPartTriangle[1] = 1;
        mainPartTriangle[2] = 2;
        mainPartTriangle[3] = 2;
        mainPartTriangle[4] = 3;
        mainPartTriangle[5] = 0;

        // Back
        mainPartTriangle[6] = 0;
        mainPartTriangle[7] = 0;
        mainPartTriangle[8] = 0;
        mainPartTriangle[9] = 0;
        mainPartTriangle[10] = 0;
        mainPartTriangle[11] = 0;

        // Left
        mainPartTriangle[12] = 0;
        mainPartTriangle[13] = 0;
        mainPartTriangle[14] = 0;
        mainPartTriangle[15] = 0;
        mainPartTriangle[16] = 0;
        mainPartTriangle[17] = 0;

        // Right
        mainPartTriangle[18] = 0;
        mainPartTriangle[19] = 0;
        mainPartTriangle[20] = 0;
        mainPartTriangle[21] = 0;
        mainPartTriangle[22] = 0;
        mainPartTriangle[23] = 0;

        // Top
        mainPartTriangle[24] = 0;
        mainPartTriangle[25] = 0;
        mainPartTriangle[26] = 0;
        mainPartTriangle[27] = 0;
        mainPartTriangle[28] = 0;
        mainPartTriangle[29] = 0;

        // Down
        mainPartTriangle[30] = 0;
        mainPartTriangle[31] = 0;
        mainPartTriangle[32] = 0;
        mainPartTriangle[33] = 0;
        mainPartTriangle[34] = 0;
        mainPartTriangle[35] = 0;
        #endregion


        #region top part
        // Front
        topPartTriangle[0] = 0;
        topPartTriangle[1] = 0;
        topPartTriangle[2] = 0;
        topPartTriangle[3] = 0;
        topPartTriangle[4] = 0;
        topPartTriangle[5] = 0;

        // Back
        topPartTriangle[6] = 0;
        topPartTriangle[7] = 0;
        topPartTriangle[8] = 0;
        topPartTriangle[9] = 0;
        topPartTriangle[10] = 0;
        topPartTriangle[11] = 0;

        // Left
        topPartTriangle[12] = 0;
        topPartTriangle[13] = 0;
        topPartTriangle[14] = 0;
        topPartTriangle[15] = 0;
        topPartTriangle[16] = 0;
        topPartTriangle[17] = 0;

        // Right
        topPartTriangle[18] = 0;
        topPartTriangle[19] = 0;
        topPartTriangle[20] = 0;
        topPartTriangle[21] = 0;
        topPartTriangle[22] = 0;
        topPartTriangle[23] = 0;

        // Top
        topPartTriangle[24] = 0;
        topPartTriangle[25] = 0;
        topPartTriangle[26] = 0;
        topPartTriangle[27] = 0;
        topPartTriangle[28] = 0;
        topPartTriangle[29] = 0;

        // Down
        topPartTriangle[30] = 0;
        topPartTriangle[31] = 0;
        topPartTriangle[32] = 0;
        topPartTriangle[33] = 0;
        topPartTriangle[34] = 0;
        topPartTriangle[35] = 0;
        #endregion


        mesh.SetTriangles(mainPartTriangle, 0);
        mesh.SetTriangles(topPartTriangle, 1);
    }

    private Vector3[] Vertices()
    {
        Vector3[] vertices = new Vector3[48];

        #region Mainpart
        // Front
        vertices[0] = P0;
        vertices[1] = P1;
        vertices[2] = P2;
        vertices[3] = P3;

        // Back
        vertices[4] = P6;
        vertices[5] = P7;
        vertices[6] = P8;
        vertices[7] = P9;

        // Left
        vertices[8] = P6;
        vertices[9] = P7;
        vertices[10] = P1;
        vertices[11] = P0;

        // Right
        vertices[4] = P3;
        vertices[5] = P2;
        vertices[6] = P8;
        vertices[7] = P9;

        // Top
        vertices[16] = P1;
        vertices[17] = P7;
        vertices[18] = P8;
        vertices[19] = P2;

        // Down
        vertices[20] = P0;
        vertices[21] = P6;
        vertices[22] = P9;
        vertices[23] = P3;
        #endregion

        #region Toppart
        // Front
        vertices[24] = P1;
        vertices[25] = P4;
        vertices[26] = P5;
        vertices[27] = P2;

        // Back
        vertices[4] = P7;
        vertices[5] = P10;
        vertices[6] = P11;
        vertices[7] = P8;

        // Left
        vertices[8] = P7;
        vertices[9] = P10;
        vertices[10] = P4;
        vertices[11] = P1;

        // Right
        vertices[8] = P2;
        vertices[9] = P5;
        vertices[10] = P11;
        vertices[11] = P8;

        // Top
        vertices[8] = P4;
        vertices[9] = P10;
        vertices[10] = P11;
        vertices[11] = P5;

        // Down
        vertices[8] = P1;
        vertices[9] = P7;
        vertices[10] = P8;
        vertices[11] = P2;
        #endregion

        return vertices;
    }

    private void UVS()
    {
        Vector3[] uvs = new Vector3[36];
    }

    private void TextureAndMaterial()
    {

    }
/*
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
*/
}
