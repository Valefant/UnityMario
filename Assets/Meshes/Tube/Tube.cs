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
        meshRenderer = GetComponent<MeshRenderer>();
        mesh = meshFilter.sharedMesh;

        if (mesh == null)
            mesh = new Mesh();

        Vector3 P0, P1, P2, P3, P4, P5, P6, P7;

        meshFilter.mesh = mesh;
    }

    private void AddVertices()
    {
    }
}
