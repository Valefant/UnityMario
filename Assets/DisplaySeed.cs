using System.Collections;
using System.Collections.Generic;
using Assets.EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySeed : MonoBehaviour
{
    private Text _text;
    
    void Start()
    {
        _text = gameObject.GetComponent<Text>();
        _text.text = "Seed: " + Assets.Game.Seed;
    }
}