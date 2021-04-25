using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Type
    {
        None,
        Power,
        Water
    }

    public GameObject powerSprite;
    public GameObject waterSprite;
    public List<MovePoint> movePoints;
    public Type type;

    public void Init(List<MovePoint> points, Enemy.Type type)
    {
        movePoints = points;
        this.type = type;

        if (type == Type.Power)
        {
            powerSprite.SetActive(true);
        } else if (type == Type.Water)
        {
            waterSprite.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}