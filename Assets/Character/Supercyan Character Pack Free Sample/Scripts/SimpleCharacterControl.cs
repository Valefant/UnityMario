using System.Collections.Generic;
using Assets.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        Tank,
        Direct
    }

    [SerializeField] public float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] public float m_jumpForce = 4;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;
    public static bool canMove = false;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    public int points = 0;

    private bool _dead = false;
    
    private AudioSource _coinAudioSource;
    private AudioSource _jumpAudioSource;
    private AudioSource _gameOverAudioSource;
    
    void Start()
    {
        _coinAudioSource = CreateAudioSourceWithClip("coin");
        _jumpAudioSource = CreateAudioSourceWithClip("jump");
        _gameOverAudioSource = CreateAudioSourceWithClip("gameOver");
        m_rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.name.ToLower().Contains("cube")) return;
        
        points++;
        Destroy(other.gameObject);
        _coinAudioSource.Play();
        EventManager.GetInstance().PublishEvent(new PickupEvent(points));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.ToLower().Contains("rabbit") || collision.gameObject.name.ToLower().Contains("ghost"))
        {
            _gameOverAudioSource.Play();
            m_animator.SetTrigger("Wave");
            _dead = true;
            EventManager.GetInstance().PublishEvent(new PickupEvent(0));
            canMove = false;
        }
        
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true;
                break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0)
            {
                m_isGrounded = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0)
        {
            m_isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (_dead)
        {
            transform.Rotate(Vector3.left * 90 * Time.deltaTime);
            
            Debug.Log(transform.rotation.eulerAngles.x);
            
            if (transform.rotation.eulerAngles.x <= 270f)
            {
                _dead = false;
                SceneManager.LoadScene("Hub");
            }
        }
        
        Vector3 oldPosition = gameObject.transform.localPosition;

        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;

        Vector3 newPosition = gameObject.transform.localPosition;
        EventManager.GetInstance().PublishEvent(new CharacterPositionUpdatedEvent(oldPosition, newPosition));
    }

    private void TankUpdate()
    {
        if (!canMove)
            return;

        float v = Input.GetAxis("Horizontal"); //Input.GetAxis("Vertical");
        //float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk)
            {
                v *= m_backwardsWalkScale;
            }
            else
            {
                v *= m_backwardRunScale;
            }
        }
        else if (walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        //m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        //transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }

    private void DirectUpdate()
    {
        if (!canMove)
            return;

        float v = Input.GetAxis("Horizontal"); //Input.GetAxis("Vertical");
        //float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            //h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        //m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            //transform.rotation = Quaternion.LookRotation(m_currentDirection);
            m_rigidBody.MovePosition(m_currentDirection * m_moveSpeed * Time.deltaTime);

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
//        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (m_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            _jumpAudioSource.Play();
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }
    
    private AudioSource CreateAudioSourceWithClip(string clipname)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = SetAudioLevels.sfxVolume;
        audioSource.clip = Resources.Load(clipname) as AudioClip;

        return audioSource;
    }
}