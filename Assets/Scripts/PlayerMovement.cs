using UnityEngine;

/// <summary>
/// Makes the player move
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get { return s_instance; } }
    public bool MovementEnabled { get; set; } = true;

    private static PlayerMovement s_instance = null;

    private Rigidbody2D m_rigidBody = null;
    private PlayerUpgradeValues m_upgradeValues = null;
    private Animator m_animator = null;
    private SpriteRenderer m_spriteRenderer = null;

    private Vector2 m_movementVector = Vector2.zero;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this);
            return;
        }
        s_instance = this;

        m_rigidBody = GetComponent<Rigidbody2D>();
        m_upgradeValues = GetComponent<PlayerUpgradeValues>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
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

        // Get input only if enabled
        if (MovementEnabled)
        {
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
        }

        UpdateAnimator(m_movementVector);
    }

    // Called on FixedUpdate
    private void Move()
    {
        m_rigidBody.AddForce(m_movementVector * m_upgradeValues.MovementForceMultiplier);

        m_movementVector = Vector2.zero;
    }

    // Called on Update
    private void UpdateAnimator(Vector2 movementVector)
    {
        bool hasInput = movementVector != Vector2.zero;
        bool isMoving = m_animator.GetBool("IsMoving");

        // Turn animation on/off
        if (hasInput && !isMoving)
        {
            m_animator.SetBool("IsMoving", true);
        }
        else if (!hasInput && isMoving)
        {
            m_animator.SetBool("IsMoving", false);
        }

        // Only update flip if moving along the X
        if (movementVector.x != 0.0f)
        {
            m_spriteRenderer.flipX = movementVector.x > 0.0f;
        }
    }
}
