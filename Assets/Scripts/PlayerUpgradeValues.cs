using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains values for player upgrades
/// </summary>
public class PlayerUpgradeValues : MonoBehaviour
{
    // Singleton
    public static PlayerUpgradeValues Instance { get { return s_instance; } }
    private static PlayerUpgradeValues s_instance = null;

    // Properties
    public float MovementForceMultiplier
    {
        get { return m_movementForceMultiplier; }
        set { m_movementForceMultiplier = value; }
    }

    public bool IsSubmarine
    {
        get { return m_isSubmarine; }
        set { m_isSubmarine = value; }
    }

    public int MaxDepth
    {
        get { return m_maxDepth; }
        set { m_maxDepth = value; }
    }

    public int MaxHealth
    {
        get { return m_maxHealth; }
        set { m_maxHealth = value; }
    }

    public float DamageOutput
    {
        get { return m_damageOutput; }
        set { m_damageOutput = value; }
    }

    public int InventorySize
    {
        get { return m_inventorySize; }
        set { m_inventorySize = value; }
    }

    // Variables

    [SerializeField]
    private float m_movementForceMultiplier = 1.0f;

    [SerializeField]
    private bool m_isSubmarine = false;

    [SerializeField]
    private int m_maxDepth = 0;

    [SerializeField]
    private int m_maxHealth = 5;

    [SerializeField]
    private float m_damageOutput = 1.0f;

    [SerializeField]
    private int m_inventorySize = 10;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this);
            return;
        }

        s_instance = this;
    }
}
