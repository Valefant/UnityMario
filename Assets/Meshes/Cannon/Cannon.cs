﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Cannon : MonoBehaviour
{
    public void Reset()
    {
        CreateMesh();
    }

    private void CreateMesh()
    {

    }
}