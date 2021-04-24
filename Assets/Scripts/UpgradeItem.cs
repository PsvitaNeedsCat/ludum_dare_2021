using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// UI item that allows the purchase of 1 item
/// </summary>
public class UpgradeItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI m_nameText = null;
    [SerializeField]
    private TextMeshProUGUI m_costText = null;
    [SerializeField]
    private GameObject m_buyText = null;
    [SerializeField]
    private Button m_buyButton = null;

    [Header("Values")]
    [SerializeField]
    private int m_cost = 999;
    [SerializeField]
    private UnityEvent m_onSuccessfulPurchase = new UnityEvent();

    private void Start()
    {
        m_costText.text = m_cost.ToString();
    }

    public void AttemptPurchase()
    {
        // Check currency
        if (true)
        {
            SuccessfulPurchase();
        }
    }

    private void SuccessfulPurchase()
    {
        // Update display
        m_buyButton.interactable = false;
        m_buyText.SetActive(false);
        m_costText.text = "Purchased";

        // Trigger event
        m_onSuccessfulPurchase.Invoke();
    }
}
