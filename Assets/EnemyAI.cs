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
        _rigidbody.AddForce(new Vector3(-0.2f, 0f, 0f), ForceMode.Impulse);
    }
}