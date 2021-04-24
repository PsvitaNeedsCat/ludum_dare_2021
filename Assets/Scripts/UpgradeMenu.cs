using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the upgrade menu high end stuff
/// </summary>
public class UpgradeMenu : MonoBehaviour
{
    public static UpgradeMenu Instance { get { return s_instance; } }

    private static UpgradeMenu s_instance = null;

    [SerializeField]
    private CanvasGroup m_canvasGroup = null;

    [SerializeField]
    private UpgradeItem[] m_depthTiers = { };

    [SerializeField]
    private UpgradeItem[] m_requiresSubmarine = { };

    private Button[] m_buttons = { };

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this);
            return;
        }

        s_instance = this;
    }

    private void Start()
    {
        m_buttons = GetComponentsInChildren<Button>();

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

        for (int i = 0; i < m_buttons.Length; i++)
        {
            m_buttons[i].interactable = enable;
        }

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

    public bool IsOpen()
    {
        return m_canvasGroup.alpha == 1.0f;
    }

    public void SellAllItems()
    {
        int value = PlayerInventory.self.SellAllItems();

        if (value != 0)
        {
            InventoryUI.Instance.SellAllItems(value);
        }
    }
}
