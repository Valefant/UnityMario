using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    Text txt;

    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = "Score: 0";
        EventManager.GetInstance()
            .AddEventHandler("PickupEvent", (e) => { txt.text = "Score: " + ((PickupEvent) e).GetPoints(); });
    }
}