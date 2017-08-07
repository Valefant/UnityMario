using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
		float maxX = 21 + (1 / 16);
		float maxY = 20 + (1 / 16);

		P0 = MyExtensions.Vector3 (3 + (11 / 16), 0, size);
		P1 = MyExtensions.Vector3 (6 + (3 / 16), 7 + (5 / 8), size);
		P2 = MyExtensions.Vector3(0, 12+(1/8) + 5/8, size);
		P3 = MyExtensions.Vector3(7+(7/8), maxY-(7+(7/8)), size);
		P4 = MyExtensions.Vector3 (maxX/2, maxY, size);
		P5 = MyExtensions.Vector3 (maxX - 7 + (7 / 8), maxY-(7+(7/8)), size);
		P6 = MyExtensions.Vector3 (maxX, 12 + (1 / 8) + 5 / 8, size);
		P7 = MyExtensions.Vector3 (maxX - 6 + (3 / 16), 7 + (5 / 8), size);
		P8 = MyExtensions.Vector3 (maxX-(3+(11/16)), 0, size);
		P9 = MyExtensions.Vector3((maxX/2), 4+(7/16), size);
		P10 = MyExtensions.Vector3 (maxX / 2, maxY / 2, depth, size);
		P11 = MyExtensions.Vector3 (maxX / 2, maxY / 2, -depth, size);
	}

	private int[] Triangles()
	{
		int[] triangle = new int[60];

		int x = 9;

		for (int i = 59; i >= 0; i--) 
		{
			if ((i+1) % 3 == 0) 
			{
				if(i<30)
					triangle [i] = 10;
				else
					triangle [i] = 11;

				if (i == 32) {
					x = 0;
					continue;
				}

				if (i > 29) {
					if (x < 9)
						x++;
				} else 
				{
					if (x > 0)
						x--;
				}

				Debug.Log("Mod i = "+i + " , X = " + triangle[i]); 
			}
			else 
			{
				triangle [i] = x;

				if (i == 32) {
					x = 0; Debug.Log("i = "+i + " , X = " + triangle[i]); 
					continue;
				} else if (i == 31) {
					x = 9; Debug.Log("i = "+i + " , X = " + triangle[i]); 
					continue;
				}else if (i == 1) {
					triangle [i] = 9; Debug.Log("i = "+i + " , X = " + triangle[i]); 
					continue;
				} else if (i == 0) {
					triangle [i] = 0; Debug.Log("i = "+i + " , X = " + triangle[i]); 
					continue;
				}

				if (i > 29) {
					if (x == 0)
						x = 9;
					else
						x--;
				} else 
				{
					if (x == 9)
						x = 0;
					else
						x++;
				}


				Debug.Log("Unten i = "+i + " , X = " + triangle[i]); 
			}
		}
		#region Front
		// 1. Dreieck

		/*
		triangle [i] = 0;
		triangle [i] = 1;


		triangle [i] = 1;
		triangle [i] = 2;
		triangle [i] = 10;

		triangle [i] = 2;
		triangle [i] = 3;
		triangle [i] = 10;

		triangle [] = 3;
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

		triangle [i] = 9;
		triangle [i] = 0;
		triangle [i] = 10;
		#endregion


		#region Back
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
		*/
		#endregion

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

	#if UNITY_EDITOR
	[MenuItem("GameObject/Primitives/Star", false, 50)]
	public static Star CreateStar()
	{
		GameObject gO = new GameObject("Star");
		Star star = gO.AddComponent<Star>();

		star.CreateMesh();

		return star;
	}
	#endif
}
