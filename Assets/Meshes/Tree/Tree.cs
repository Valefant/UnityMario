using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Tree : MonoBehaviour
{
	#region Mesh
	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	Mesh mesh; 
	#endregion

	// Positions
	// P10 = Center Front
	// P11 = Center Back
	public Vector3 P0, P1, P2, P3, P4, 
	P5, P6, P7, P8, P9, P10, P11;


	#region Lists - vertices, triangles, uvs
	List<Vector3> vertices = new List<Vector3>();
	List<int> triangles = new List<int>();
	List<Vector2> uvs = new List<Vector2>();
	#endregion

	#region Parameter
	public float diameter = 3;
	public float size = 10f;
	#endregion

	public Texture2D mainTexture;

	public void Reset()
	{
		CreateMesh();
	}

	public void CreateMesh()
	{
		Mesh ();
		Clear ();
		Positions ();
		int[] triangle = Triangles ();
		Vector3[] vertices = Vertices ();

		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.SetTriangles(triangle,0);
		// uvs
		mesh.RecalculateNormals();
		meshFilter.mesh = mesh;


	}

	private void Mesh()
	{
		meshFilter = GetComponent<MeshFilter> ();

		if (meshFilter == null)
			meshFilter = this.gameObject.AddComponent<MeshFilter> ();


		meshRenderer = GetComponent<MeshRenderer> ();

		if (meshRenderer == null)
			meshRenderer = this.gameObject.AddComponent<MeshRenderer>();


		mesh = meshFilter.sharedMesh;

		if (mesh == null)
			mesh = new Mesh ();		
	}

	private void Clear()
	{
		vertices.Clear ();
		uvs.Clear ();
		triangles.Clear ();
	}

	private void Positions()
	{

		//P0 = MyExtensions.Vector3(0, 0, size);
	}

	private int[] Triangles()
	{
		int[] triangle = new int[60];

		return triangle;
	}

	private Vector3[] Vertices()
	{
		Vector3[] vertices = new Vector3[12]; 
		vertices [0] = P0;

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
	[MenuItem("GameObject/Primitives/Tree", false, 50)]
	public static Tree CreateTree()
	{
		GameObject gO = new GameObject("Tree");
		Tree tree = gO.AddComponent<Tree>();

		tree.CreateMesh();

		return tree;
	}
	#endif
}
