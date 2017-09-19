using Assets.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class MeterCountUpdater : MonoBehaviour
    {
        private Text meterCount;
        
        void Start()
        {
            meterCount = gameObject.GetComponent<Text>();
            meterCount.text = "Meter: 0";
            EventManager.GetInstance().AddEventHandler("CharacterPositionUpdatedEvent", e =>
            {
                var newPosition = (int) (e as CharacterPositionUpdatedEvent).GetNewPosition().x;
                meterCount.text = "Meter: " + newPosition;
            });
        }
    }
}