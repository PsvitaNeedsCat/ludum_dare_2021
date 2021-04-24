using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the player move
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_rigidBody = null;
    private PlayerUpgradeValues m_upgradeValues = null;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_upgradeValues = GetComponent<PlayerUpgradeValues>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Called on FixedUpdate
    private void Move()
    {
        // Get movement vector
        Vector3 movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementVector.y += 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector.y -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x += 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x -= 1.0f;
        }

        m_rigidBody.AddForce(movementVector * m_upgradeValues.MovementForceMultiplier);
    }
}
