using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public ItemData data;

    public Image pickupBar;

    private bool isPickedUp = false;

    public void Pickup()
    {
        transform.DOScale(1.3f, 0.5f);

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOBlendableLocalMoveBy(Vector3.up * 0.5f, 0.5f));
        seq.Append(transform.DOScale(0.0f, 0.5f));

        isPickedUp = true;
    }

    public bool HasBeenPickedUp()
    {
        return isPickedUp;
    }

    public void SetPickedUpAmount(float amount)
    {
        pickupBar.fillAmount = amount;
    }
}
