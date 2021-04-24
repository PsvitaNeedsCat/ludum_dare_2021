using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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

    [SerializeField]
    private TextMeshProUGUI m_moneyTween = null;

    private Sequence m_moneyAnimation = null;
    private int m_moneyAnimationValue = 0;

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
        m_moneyAnimation = DOTween.Sequence();

        m_moneyAnimation.Append(m_moneyTween.GetComponent<CanvasGroup>().DOFade(1.0f, 0.5f))
            .AppendInterval(1.0f)
            .Append(m_moneyTween.transform.DOLocalMoveY(m_moneyTween.transform.localPosition.y + 100.0f, 0.5f))
            .Append(m_moneyTween.GetComponent<CanvasGroup>().DOFade(0.0f, 0.1f));

        m_moneyAnimation.OnComplete(() =>
        {
            UpdateMoney(PlayerInventory.self.playerCurrency);
        });

        m_moneyAnimation.Rewind();
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

    public void SellAllItems(int value)
    {
        for (int i = 0; i < m_itemsText.Length; i++)
        {
            m_itemsText[i].text = "0";
        }

        m_totalItemsText.text = "0";

        m_moneyAnimationValue = value;
        m_moneyTween.text = "+" + value.ToString();
        m_moneyAnimation.Rewind();
        m_moneyAnimation.Play();
    }
}
