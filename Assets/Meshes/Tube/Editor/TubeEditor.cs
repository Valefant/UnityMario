using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tube))]
public class TubeEditor : Editor
{
    Tube tube;

    void Awake()
    {
        tube = target as Tube;
    }
}
