using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{   
    private ItemPickup targetItem = null;

    private float startPickupTime = 0.0f;

    private List<ItemPickup> nearbyItems = new List<ItemPickup>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (targetItem == null)
            {
                TryPickup();
            }            
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (targetItem != null)
            {
                UpdatePickup();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && targetItem != null)
        {
            CancelPickingUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemPickup item = collision.GetComponent<ItemPickup>();

        if (item)
        {
            if (!nearbyItems.Contains(item))
            {
                nearbyItems.Add(item);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ItemPickup item = collision.GetComponent<ItemPickup>();

        if (item)
        {
            if (nearbyItems.Contains(item))
            {
                nearbyItems.Remove(item);
            }
        }
    }

    private ItemPickup GetClosestItem()
    {
        ItemPickup closest = null;
        float lowestDist = float.MaxValue;
        foreach (ItemPickup item in nearbyItems)
        {
            if (item.HasBeenPickedUp())
            {
                continue;
            }

            Vector2 toItem = item.transform.position - transform.position;
            float dist = toItem.magnitude;
            if (dist < lowestDist)
            {
                closest = item;
                lowestDist = dist;
            }
        }

        return closest;
    }

    private void TryPickup()
    {
        ItemPickup closest = GetClosestItem();
        if (closest != null)
        {
            targetItem = closest;

            // Pickup instant items right away
            if (targetItem.data.collectionTime < 0.01f)
            {
                PickupItem();
            }
            else
            {
                startPickupTime = Time.time;
                targetItem.pickupBar.gameObject.SetActive(true);
            }
        }
    }

    private void UpdatePickup()
    {
        ItemPickup closest = GetClosestItem();
        if (closest == null)
        {
            CancelPickingUp();
        }
        else if (targetItem != closest)
        {
            CancelPickingUp();
            targetItem = closest;
            startPickupTime = Time.time;
            targetItem.pickupBar.gameObject.SetActive(true);
        }

        if (targetItem == null)
        {
            return;
        }

        if (GetPickingUpTime() >= targetItem.data.collectionTime)
        {
            PickupItem();
        }
        else
        {
            targetItem.SetPickedUpAmount(Mathf.Clamp01(GetPickingUpTime() / targetItem.data.collectionTime));
        }
    }

    private void PickupItem()
    {
        PlayerInventory.self.AddItem(targetItem.data, 1);
        targetItem.pickupBar.gameObject.SetActive(false);
        targetItem.Pickup();
        targetItem = null;
    }

    private void CancelPickingUp()
    {
        targetItem.pickupBar.gameObject.SetActive(false);
        targetItem = null;
    }

    private float GetPickingUpTime()
    {
        return Time.time - startPickupTime;
    }
}
