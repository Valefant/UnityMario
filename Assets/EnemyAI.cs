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
            _rigidbody.AddForce(new Vector3(1f, 0f, 0f), ForceMode.Impulse);
        }
        else
        {
            _rigidbody.AddForce(new Vector3(-1f, 0f, 0f), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.name .ToLower().StartsWith("rabbit") || gameObject.name .ToLower().StartsWith("ghost"))
        {
            ChangeDirection();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "invisibleBox")
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if (_rigidbody == null)
        {
            return;
        }
        
        _rigidbody.velocity = Vector3.zero;
        float yDir = _rigidbody.rotation.y > 0 ? -90 : 90;
        gameObject.transform.rotation = Quaternion.AngleAxis(yDir, new Vector3(0, 1, 0));        
    }
}