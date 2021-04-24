using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemData : ScriptableObject
{
    public enum Type
    {
        None,
        Seaweed,
        Shell
    }

    public Type type;

    // Non-zero collection times require you to hold E
    public float collectionTime = 0.0f;

    // How much the item is worth
    public float value = 0.0f;
}
