using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the player move
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidBody = null;
    private PlayerUpgradeValues m_upgradeValues = null;
    private Animator m_animator = null;

    private Vector2 m_movementVector = Vector2.zero;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_upgradeValues = GetComponent<PlayerUpgradeValues>();
        m_animator = GetComponent<Animator>();
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
        m_movementVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            m_movementVector.y += 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_movementVector.y -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_movementVector.x += 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_movementVector.x -= 1.0f;
        }

        UpdateAnimator(m_movementVector != Vector2.zero);
    }

    // Called on FixedUpdate
    private void Move()
    {
        m_rigidBody.AddForce(m_movementVector * m_upgradeValues.MovementForceMultiplier);

        m_movementVector = Vector2.zero;
    }

    // Called on Update
    private void UpdateAnimator(bool hasInput)
    {
        bool isMoving = m_animator.GetBool("IsMoving");

        if (hasInput && !isMoving)
        {
            m_animator.SetBool("IsMoving", true);
        }
        else if (!hasInput && isMoving)
        {
            m_animator.SetBool("IsMoving", false);
        }
    }
}
