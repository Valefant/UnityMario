using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {

    Text txt;
    
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = "Score: ";
		TheEventSystem.getInstance().addEventHandler("PickupEvent", (e) => {
			txt.text = "Score :" + ((PickupEvent)e).getPoints();
		});
    }
}
