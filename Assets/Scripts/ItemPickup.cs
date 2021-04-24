using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemPickup : MonoBehaviour
{
    public ItemData data;

    public GameObject pickupPrompt;

    private bool isPickedUp = false;

    public void Pickup()
    {


        isPickedUp = true;
    }
}
