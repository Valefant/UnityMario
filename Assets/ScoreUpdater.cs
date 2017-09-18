using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {

    Text txt;
    TheEventSystem.EventHandler eventHandler;
    // Use this for initialization
    void Start()
    {
        TheEventSystem tES = TheEventSystem.getInstance();
        txt = gameObject.GetComponent<Text>();

        eventHandler = (e) => txt.text = ((PickupEvent)e).getPoints().ToString();
        txt.text = "Score: ";
        tES.addEventHandler(typeof(PickupEvent).GetType().Name, (e) => txt.text = "Score :" + ((PickupEvent)e).getPoints());
        

        
        //txt.text = "Score : ";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
