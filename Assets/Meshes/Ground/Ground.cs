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
    public Texture2D mainTexture;
    public Texture2D topTexture;

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
        UVS();
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
        P6 = MyExtensions.Vector3(0,0,1, Width, Height, Depth);
        P7 = MyExtensions.Vector3(0,1*groundHeight,1, Width, Height, Depth);
        P8 = MyExtensions.Vector3(1,1*groundHeight,1, Width, Height, Depth);
        P9 = MyExtensions.Vector3(1,0,1, Width, Height, Depth);

        // Submesh
        P4 = MyExtensions.Vector3(0, 1, 0, Width, Height, Depth); // Submesh
        P5 = MyExtensions.Vector3(1, 1, 0, Width, Height, Depth); // Submesh
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
        mainPartTriangle[6] = 6;
        mainPartTriangle[7] = 5;
        mainPartTriangle[8] = 4;

        mainPartTriangle[9] = 4;
        mainPartTriangle[10] = 7;
        mainPartTriangle[11] = 6;

        // Left
        mainPartTriangle[12] = 8;
        mainPartTriangle[13] = 9;
        mainPartTriangle[14] = 10;
        mainPartTriangle[15] = 10;
        mainPartTriangle[16] = 11;
        mainPartTriangle[17] = 8;

        // Right
        mainPartTriangle[18] = 12;
        mainPartTriangle[19] = 13;
        mainPartTriangle[20] = 14;
        mainPartTriangle[21] = 14;
        mainPartTriangle[22] = 15;
        mainPartTriangle[23] = 12;

        // Top
        mainPartTriangle[24] = 16;
        mainPartTriangle[25] = 17;
        mainPartTriangle[26] = 18;
        mainPartTriangle[27] = 18;
        mainPartTriangle[28] = 19;
        mainPartTriangle[29] = 16;

        // Down
        mainPartTriangle[30] = 22;
        mainPartTriangle[31] = 21;
        mainPartTriangle[32] = 20;
        mainPartTriangle[33] = 20;
        mainPartTriangle[34] = 23;
        mainPartTriangle[35] = 22;
        #endregion


        #region top part
        // Front
        topPartTriangle[0] = 24;
        topPartTriangle[1] = 25;
        topPartTriangle[2] = 26;
        topPartTriangle[3] = 26;
        topPartTriangle[4] = 27;
        topPartTriangle[5] = 24;

        // Back
        topPartTriangle[6] = 30;
        topPartTriangle[7] = 29;
        topPartTriangle[8] = 28;
        topPartTriangle[9] = 28;
        topPartTriangle[10] = 31;
        topPartTriangle[11] = 30;

        // Left
        topPartTriangle[12] = 32;
        topPartTriangle[13] = 33;
        topPartTriangle[14] = 34;
        topPartTriangle[15] = 34;
        topPartTriangle[16] = 35;
        topPartTriangle[17] = 32;

        // Right
        topPartTriangle[18] = 36;
        topPartTriangle[19] = 37;
        topPartTriangle[20] = 38;
        topPartTriangle[21] = 38;
        topPartTriangle[22] = 39;
        topPartTriangle[23] = 36;

        // Top
        topPartTriangle[24] = 40;
        topPartTriangle[25] = 41;
        topPartTriangle[26] = 42;
        topPartTriangle[27] = 42;
        topPartTriangle[28] = 43;
        topPartTriangle[29] = 40;

        // Down
        topPartTriangle[30] = 44;
        topPartTriangle[31] = 45;
        topPartTriangle[32] = 46;
        topPartTriangle[33] = 44;
        topPartTriangle[34] = 47;
        topPartTriangle[35] = 46;
        #endregion

        mesh.subMeshCount = 2;
        mesh.SetTriangles(mainPartTriangle, 1);
        mesh.SetTriangles(topPartTriangle, 0);
        TextureAndMaterial();
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
        vertices[12] = P3;
        vertices[13] = P2;
        vertices[14] = P8;
        vertices[15] = P9;

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
        vertices[28] = P7;
        vertices[29] = P10;
        vertices[30] = P11;
        vertices[31] = P8;

        // Left
        vertices[32] = P7;
        vertices[33] = P10;
        vertices[34] = P4;
        vertices[35] = P1;

        // Right
        vertices[36] = P2;
        vertices[37] = P5;
        vertices[38] = P11;
        vertices[39] = P8;

        // Top
        vertices[40] = P4;
        vertices[41] = P10;
        vertices[42] = P11;
        vertices[43] = P5;

        // Down
        vertices[44] = P1;
        vertices[45] = P7;
        vertices[46] = P8;
        vertices[47] = P2;
        #endregion

        return vertices;
    }

    private void UVS()
    {
        Vector2[] uv = new Vector2[48];

        #region Mainpart
        // Front
        uv[0] = new Vector2(0f, 0f);
        uv[1] = new Vector2(0f, Height*groundHeight);
        uv[2] = new Vector2(Width, Height * groundHeight);
        uv[3] = new Vector2(Width, 0);

        // Back
        uv[4] = new Vector2(0f, 0f);
        uv[5] = new Vector2(0f, Height * groundHeight);
        uv[6] = new Vector2(Width, Height * groundHeight);
        uv[7] = new Vector2(Width, 0f);

        // Left
        uv[8] = new Vector2(0f, 0f);
        uv[9] = new Vector2(0f, Height * groundHeight);
        uv[10] = new Vector2(Depth, Height * groundHeight);
        uv[11] = new Vector2(Depth, 0f);

        // Right
        uv[12] = new Vector2(0f, 0f);
        uv[13] = new Vector2(0f, Height * groundHeight);
        uv[14] = new Vector2(Depth, Height * groundHeight);
        uv[15] = new Vector2(Depth, 0f);

        // Top
        uv[16] = new Vector2(0f, 0f);
        uv[17] = new Vector2(0f, Depth);
        uv[18] = new Vector2(Width, Depth);
        uv[19] = new Vector2(Width, 0);

        // Down
        uv[20] = new Vector2(0f, 0f);
        uv[21] = new Vector2(0f, Depth);
        uv[22] = new Vector2(Width, Depth);
        uv[23] = new Vector2(Width, 0);
        #endregion
        /*
        #region Toppart
        // Front
        uv[24] = P1;
        uv[25] = P4;
        uv[26] = P5;
        uv[27] = P2;

        // Back
        uv[28] = P7;
        uv[29] = P10;
        uv[30] = P11;
        uv[31] = P8;

        // Left
        uv[32] = P7;
        uv[33] = P10;
        uv[34] = P4;
        uv[35] = P1;

        // Right
        uv[36] = P2;
        uv[37] = P5;
        uv[38] = P11;
        uv[39] = P8;

        // Top
        uv[40] = P4;
        uv[41] = P10;
        uv[42] = P11;
        uv[43] = P5;

        // Down
        uv[44] = P1;
        uv[45] = P7;
        uv[46] = P8;
        uv[47] = P2;
        #endregion
        */

        mesh.uv = uv;
    }

    private void TextureAndMaterial()
    {
        // TODO: Texturen laden
        Texture[] textures = new Texture[mesh.subMeshCount];
        textures[1] = Resources.Load("dirt stones leafs more") as Texture;
        textures[0] = Resources.Load("leafs dark") as Texture;

        // TODO: Material-Array anlegen
        Material[] materials = new Material[mesh.subMeshCount];

        // TODO: Materialien erstellen
        // TODO: Den Materialien die Textur zuweisen
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            materials[i] = new Material(Shader.Find("Diffuse"));
            materials[i].mainTexture = textures[i];
        }

        // TODO: Dem MeshRenderer das MaterialArray übergeben
        meshRenderer.materials = materials;
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
