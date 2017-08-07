using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

namespace ExtensionMethods
{
	public static class MyExtensions
	{
		public static Vector3 Vector3(float x, float y, float size)
		{
			return new Vector3 (x * size, y * size);
		}

		public static Vector3 Vector3(float x, float y, float z, float size)
		{
			return new Vector3 (x * size, y * size, z * size);
		}
	}   
}



[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Star : MonoBehaviour
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
	public float depth = 3;
	public float Size = 10f;
	#endregion

	public Texture2D mainTexture;

    public void Reset()
    {
        CreateMesh();
    }

    private void CreateMesh()
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
		float maxX = 21 + (1 / 16);
		float maxY = 20 + (1 / 16);

		P0 = MyExtensions.Vector3 (3 + (11 / 16), 0, Size);
		P1 = MyExtensions.Vector3 (6 + (3 / 16), 7 + (5 / 8), Size);
		P2 = MyExtensions.Vector3(0, 12+(1/8) + 5/8, Size);
		P3 = MyExtensions.Vector3(7+(7/8), maxY-(7+(7/8)), Size);
		P4 = MyExtensions.Vector3 (maxX/2, maxY, Size);
		P5 = MyExtensions.Vector3 (maxX - 7 + (7 / 8), maxY-(7+(7/8)), Size);
		P6 = MyExtensions.Vector3 (maxX, 12 + (1 / 8) + 5 / 8, Size);
		P7 = MyExtensions.Vector3 (maxX - 6 + (3 / 16), 7 + (5 / 8), Size);
		P8 = MyExtensions.Vector3 (maxX-(3+(11/16)), 0, Size);
		P9 = MyExtensions.Vector3((maxX/2), 4+(7/16), Size);
		P10 = MyExtensions.Vector3 (maxX / 2, maxY / 2, depth, Size);
		P11 = MyExtensions.Vector3 (maxX / 2, maxY / 2, -depth, Size);
	}

	private int[] Triangles()
	{
		int[] triangle = new int[60];

		// Front
		// 1. Dreieck
		triangle [0] = 9;
		triangle [1] = 0;
		triangle [2] = 10;

		triangle [3] = 0;
		triangle [4] = 1;
		triangle [5] = 10;

		triangle [6] = 1;
		triangle [7] = 2;
		triangle [8] = 10;

		triangle [9] = 2;
		triangle [10] = 3;
		triangle [11] = 10;

		triangle [12] = 3;
		triangle [13] = 4;
		triangle [14] = 10;

		triangle [15] = 4;
		triangle [16] = 5;
		triangle [17] = 10;

		triangle [18] = 5;
		triangle [19] = 6;
		triangle [20] = 10;

		triangle [21] = 6;
		triangle [22] = 7;
		triangle [23] = 10;

		triangle [24] = 7;
		triangle [25] = 8;
		triangle [26] = 10;

		// 10. Dreieck
		triangle [27] = 8;
		triangle [28] = 9;
		triangle [29] = 10;



		// Back
		// 11. Dreieck
		triangle [30] = 0;
		triangle [31] = 9;
		triangle [32] = 11;

		triangle [33] = 1;
		triangle [34] = 0;
		triangle [35] = 11;

		triangle [36] = 2;
		triangle [37] = 1;
		triangle [38] = 11;

		triangle [39] = 3;
		triangle [40] = 2;
		triangle [41] = 11;

		triangle [42] = 4;
		triangle [43] = 3;
		triangle [44] = 11;

		triangle [45] = 5;
		triangle [46] = 4;
		triangle [47] = 11;

		triangle [48] = 6;
		triangle [49] = 5;
		triangle [50] = 11;

		triangle [51] = 7;
		triangle [52] = 6;
		triangle [53] = 11;

		triangle [54] = 8;
		triangle [55] = 7;
		triangle [56] = 11;

		// 20. Dreieck
		triangle [57] = 9;
		triangle [58] = 8;
		triangle [59] = 11;

		return triangle;
	}

	private Vector3[] Vertices()
	{
		Vector3[] vertices = new Vector3[12]; 
		vertices [0] = P0;
		vertices [1] = P1;
		vertices [2] = P2;
		vertices [3] = P3;
		vertices [4] = P4;
		vertices [5] = P5;
		vertices [6] = P6;
		vertices [7] = P7;
		vertices [8] = P8;
		vertices [9] = P9;
		vertices [10] = P10;
		vertices [11] = P11;

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
	[MenuItem("GameObject/Primitives/Star", false, 50)]
	public static Star CreateStar()
	{
		GameObject gO = new GameObject("Star");
		Star star = gO.AddComponent<Star>();

		star.CreateMesh();

		return star;
	}
	#endif */
}
