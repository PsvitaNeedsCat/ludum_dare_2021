using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : MonoBehaviour
{
    [SerializeField]
    private float m_maxDistanceFromPlayer = 1.0f;

    [SerializeField]
    private GameObject m_interactKey = null;

    private void Update()
    {
        float distance = (PlayerMovement.Instance.transform.position - transform.position).magnitude;

        if (distance <= m_maxDistanceFromPlayer)
        {
            // Turn on E
            if (!m_interactKey.activeSelf)
            {
                m_interactKey.SetActive(true);
            }

            // Toggle menu
            if (Input.GetKeyDown(KeyCode.E) && !UpgradeMenu.Instance.IsOpen())
            {
                UpgradeMenu.Instance.ToggleMenu(true);
            }
        }
        else
        {
            // Turn off E
            if (m_interactKey.activeSelf)
            {
                m_interactKey.SetActive(false);
            }
        }
    }
}
