using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Tube : MonoBehaviour
{
    public List<Vector3> vertices = new List<Vector3>();
    public float height;

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Mesh mesh;

    public void Reset()
    {
        CreateMesh();
    }

    private void CreateMesh()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = this.gameObject.AddComponent<MeshFilter>();

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();

        this.mesh = meshFilter.sharedMesh;

        if (this.mesh == null)
            this.mesh = new Mesh();

        AddVertices();

        mesh.vertices = vertices.ToArray();
        meshFilter.mesh = mesh;
    }

    private void AddVertices()
    {
        Vector3 P0 = new Vector3(0.0f, 0f, 0.5f);
        vertices.Add(P0);

        Vector3 P1 = new Vector3(0.5f, 0f, 0f);
        vertices.Add(P1);

        Vector3 P2 = new Vector3(1.0f, 0f, 0.0f);
        vertices.Add(P2);

        Vector3 P3 = new Vector3(1.5f, 0f, 0.5f);
        vertices.Add(P3);

        Vector3 P4 = new Vector3(1.5f, 0f, 1f);
        vertices.Add(P4);

        Vector3 P5 = new Vector3(1f, 0f, 1.5f);
        vertices.Add(P5);

        Vector3 P6 = new Vector3(0.5f, 0f, 1.5f);
        vertices.Add(P6);

        Vector3 P7 = new Vector3(0f, 0f, 1f);
        vertices.Add(P7);

        Vector3 P8 = P0 + new Vector3(-0.5f, 0f, 0f);
        vertices.Add(P8);

        Vector3 P9 = P1 + new Vector3(0f, 0f, -0.5f);
        vertices.Add(P9);

        Vector3 P10 = P2 + new Vector3(0f, 0f, -0.5f);
        vertices.Add(P10);

        Vector3 P11 = P3 + new Vector3(0.5f, 0f, 0f);
        vertices.Add(P11);

        Vector3 P12 = P4 + new Vector3(0.5f, 0f, 0f);
        vertices.Add(P12);

        Vector3 P13 = P5 + new Vector3(0f, 0f, 0.5f);
        vertices.Add(P13);

        Vector3 P14 = P6 + new Vector3(0f, 0f, 0.5f);
        vertices.Add(P14);

        Vector3 P15 = P7 + new Vector3(-0.5f, 0f, 0f);
        vertices.Add(P15);

        // Add another 8 vertices. These are only shifted on the y axis (height)
        foreach (Vector3 vertex in vertices.GetRange(8, 8))
        {
            vertices.Add(new Vector3(vertex.x, height, vertex.z));
        }
    }
}
