using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    List<GameObject> GroundTiles = new List<GameObject>();

	// Use this for initialization
	void Start () {



    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Reset()
    {
        GenerateGround(Vector3.zero);
    }

    void GenerateGround(Vector3 position, int Width = 1, int Height = 1, int Depth = 1, float groundHeight = 0.8f)
    {
        GameObject gameObject = new GameObject();

        gameObject.AddComponent<Ground>();
        gameObject.GetComponent<Ground>().Width = Width;
        gameObject.GetComponent<Ground>().Height = Height;
        gameObject.GetComponent<Ground>().Depth = Depth;
        gameObject.GetComponent<Ground>().groundHeight = groundHeight;

        GroundTiles.Add(gameObject);
    }
}
