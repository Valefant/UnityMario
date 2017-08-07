using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cannon))]
public class CannonEditor : Editor
{
    Cannon cannon;

    void Awake()
    {
        cannon = target as Cannon;        
    }
}
