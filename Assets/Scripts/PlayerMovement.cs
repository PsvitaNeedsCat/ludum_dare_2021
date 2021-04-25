using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float m_moveForce = 1.0f;
    [SerializeField]
    private float m_dashForce = 10.0f;

    private Rigidbody2D m_rigidBody = null;

    private Vector2 m_movementDirection = Vector2.zero;
    private Vector2 m_dashDirection = Vector2.zero;

    private void Awake()
    {
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
    }

    // Called on FixedUpdate
    private void Move()
    {
        m_rigidBody.AddForce(m_movementDirection * m_moveForce);
        m_movementDirection = Vector2.zero;

        m_rigidBody.AddForce(m_dashDirection * m_dashForce, ForceMode2D.Impulse);
        m_dashDirection = Vector2.zero;
    }
}
