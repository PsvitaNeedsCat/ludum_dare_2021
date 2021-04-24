using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Updates the UI display for inventory, including money
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get { return s_instance; } }

    private static InventoryUI s_instance = null;

    [SerializeField]
    private TextMeshProUGUI m_moneyText = null;

    [SerializeField]
    private TextMeshProUGUI[] m_itemsText = { };

    [SerializeField]
    private TextMeshProUGUI m_totalItemsText = null;

    [SerializeField]
    private TextMeshProUGUI m_maxItemsText = null;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this);
            return;
        }
        s_instance = this;
    }

    public void UpdateMoney(float newAmount)
    {
        m_moneyText.text = newAmount.ToString();
    }

    public void UpdateItem(ItemData.Type itemType, int newAmount, int totalItems)
    {
        m_itemsText[((int)itemType) - 1].text = newAmount.ToString();

        m_totalItemsText.text = totalItems.ToString();
    }

    public void UpdateInventorySize(int newSize)
    {
        m_maxItemsText.text = newSize.ToString();
    }
}
