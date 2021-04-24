using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubmarine : MonoBehaviour
{
    public RuntimeAnimatorController playerAnimator;
    public RuntimeAnimatorController submarineAnimator;

    public Vector2 submarineSolidColliderSize;
    public float submarinePickupRadius;
    
    private BoxCollider2D solidCollider;
    private CircleCollider2D pickupCollider;
    private Animator animator;

    private Vector2 playerSolidColliderSize;
    private float playerPickupRadius;
    private bool isActive = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        solidCollider = GetComponent<BoxCollider2D>();
        pickupCollider = GetComponent<CircleCollider2D>();

        playerSolidColliderSize = solidCollider.size;
        playerPickupRadius = pickupCollider.radius;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (isActive)
            {
                Disable();
            }
            else
            {
                Enable();
            }
            isActive = !isActive;
        }
    }

    public void Enable()
    {
        animator.runtimeAnimatorController = submarineAnimator;
        solidCollider.size = submarineSolidColliderSize;
        pickupCollider.radius = submarinePickupRadius;
    }

    public void Disable()
    {
        animator.runtimeAnimatorController = playerAnimator;
        solidCollider.size = playerSolidColliderSize;
        pickupCollider.radius = playerPickupRadius;
    }
}
