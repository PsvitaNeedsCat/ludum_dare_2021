using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory self = null;

    public float playerCurrency = 0.0f;

    private Dictionary<ItemData.Type, int> inventory = new Dictionary<ItemData.Type, int>();

    private int m_totalItems = 0;

    private void Awake()
    {
        if (self != null && self != this)
        {
            Destroy(gameObject);
            return;
        }
        self = this;
    }

    public void AddItem(ItemData data, int amount)
    {
        if (!inventory.ContainsKey(data.type))
        {
            inventory.Add(data.type, 0);
        }

        inventory[data.type] = inventory[data.type] + amount;
        m_totalItems += amount;

        InventoryUI.Instance.UpdateItem(data.type, inventory[data.type], m_totalItems);

        // Update currency
        playerCurrency += data.value * amount;
        InventoryUI.Instance.UpdateMoney(playerCurrency);
    }
}
