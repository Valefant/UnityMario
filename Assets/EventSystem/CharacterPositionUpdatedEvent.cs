using UnityEngine;

namespace Assets.EventSystem
{
    public class CharacterPositionUpdatedEvent : IEventable
    {
        private Vector3 _oldPosition;
        private Vector3 _newPosition;

        public CharacterPositionUpdatedEvent(Vector3 oldPosition, Vector3 newPosition)
        {
            _oldPosition = oldPosition;
            _newPosition = newPosition;
        }

        public Vector3 GetOldPosition()
        {
            return _oldPosition;
        }
        
        public Vector3 GetNewPosition()
        {
            return _newPosition;
        }

        public string GetName()
        {
            return "CharacterPositionUpdatedEvent";
        }
    }
}