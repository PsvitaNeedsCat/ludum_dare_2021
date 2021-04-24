using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains values for player upgrades
/// </summary>
public class PlayerUpgradeValues : MonoBehaviour
{
    public float MovementForceMultiplier
    {
        get { return m_movementForceMultiplier; }
        set { m_movementForceMultiplier = value; }
    }

    [SerializeField]
    private float m_movementForceMultiplier = 1.0f;
}
