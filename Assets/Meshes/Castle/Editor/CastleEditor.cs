using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Castle))]
public class CastleEditor : Editor
{
    Castle castle;

    void Awake()
    {
        castle = target as Castle;
    }
}
