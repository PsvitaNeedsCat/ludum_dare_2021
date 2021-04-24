using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the upgrade menu high end stuff
/// </summary>
public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup m_canvasGroup = null;

    [SerializeField]
    private UpgradeItem[] m_depthTiers = { };

    [SerializeField]
    private UpgradeItem[] m_requiresSubmarine = { };

    private void Start()
    {
        // Disable locked items
        for (int i = 0; i < m_requiresSubmarine.Length; i++)
        {
            m_requiresSubmarine[i].gameObject.SetActive(false);
        }

        for (int i = 1; i < m_depthTiers.Length; i++)
        {
            m_depthTiers[i].gameObject.SetActive(false);
        }

        // Hide menu
        ToggleMenu(false);
    }

    public void ToggleMenu(bool enable)
    {
        m_canvasGroup.alpha = (enable) ? 1.0f : 0.0f;

        PlayerMovement.Instance.MovementEnabled = !enable;
    }

    public void UnlockSubmarine()
    {
        for (int i = 0; i < m_requiresSubmarine.Length; i++)
        {
            m_requiresSubmarine[i].gameObject.SetActive(true);
        }
    }

    public void UgradeDepth(int tier)
    {
        for (int i = 0; i < m_depthTiers.Length; i++)
        {
            m_depthTiers[i].gameObject.SetActive(i == tier);
        }
    }
}
