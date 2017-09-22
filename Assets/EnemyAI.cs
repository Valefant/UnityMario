using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (_rigidbody.rotation.y > 0)
        {
            _rigidbody.AddForce(new Vector3(0.25f, 0f, 0f), ForceMode.Impulse);
        } else
        {
            _rigidbody.AddForce(new Vector3(-0.25f, 0f, 0f), ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_rigidbody == null)
        {
            return;
        }

        if (other.gameObject.name == "invisibleBox" || other.gameObject.name.ToLower().Contains("rabbit"))
        {
            float yDir = _rigidbody.rotation.y > 0 ? -90 : 90; 
            _rigidbody.MoveRotation(Quaternion.AngleAxis(yDir, new Vector3(0, 1, 0)));
        }
    }
}