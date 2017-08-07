using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Star))]
public class StarEditor : Editor
{
    Star star;    

    void Awake()
    {
        star = target as Star;
    }
}
