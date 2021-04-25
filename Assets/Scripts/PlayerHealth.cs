using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int m_initHealth = 3;

    [SerializeField]
    private int m_initMaxHealth = 3;

    private void Start()
    {
        HealthBar.Instance.Health = m_initHealth;
        HealthBar.Instance.MaxHealth = m_initMaxHealth;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        HealthBar.Instance.Health -= 1;
    //    }
    //}
}
