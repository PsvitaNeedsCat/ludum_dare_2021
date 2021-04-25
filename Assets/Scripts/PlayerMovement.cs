using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get { return s_instance; } }

    public GameObject sprites;
    public Animator topAnimator;
    public Animator bottomAnimator;

    [SerializeField]
    private float m_moveForce = 1.0f;
    [SerializeField]
    private float m_dashForce = 10.0f;

    private Rigidbody2D m_rigidBody = null;

    private Vector2 m_movementDirection = Vector2.zero;
    private Vector2 m_dashDirection = Vector2.zero;

    private static PlayerMovement s_instance = null;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this);
            return;
        }
        s_instance = this;

        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Called on Update
    private void UpdateInput()
    {
        m_movementDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            m_movementDirection.y += 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_movementDirection.y -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_movementDirection.x += 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_movementDirection.x -= 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_dashDirection = m_movementDirection * m_dashForce;
        }

        float animatorSpeed = (m_movementDirection.magnitude < 0.1f) ? 0.0f : 0.2f;

        topAnimator.speed = animatorSpeed;
        bottomAnimator.speed = animatorSpeed;

        if (m_movementDirection.magnitude > 0.1f)
        {
            float angle = Vector2.SignedAngle(Vector2.up, m_movementDirection);
            sprites.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }
    }

    // Called on FixedUpdate
    private void Move()
    {
        m_rigidBody.AddForce(m_movementDirection.normalized * m_moveForce);
        m_movementDirection = Vector2.zero;

        m_rigidBody.AddForce(m_dashDirection * m_dashForce, ForceMode2D.Impulse);
        m_dashDirection = Vector2.zero;
    }
}
