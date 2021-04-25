using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Init(List<MovePoint> points, Type type)
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

        Sequence seq = DOTween.Sequence();

        for (int i = 1; i < points.Count; i++)
        {
            MovePoint point = points[i];
            Debug.Log("Moving enemy to: " + point.targetPoint + " over: " + point.moveDuration);
            seq.Append(rigidBody.DOMove(point.targetPoint, point.moveDuration));
        }

        seq.AppendCallback(() => Destroy(this.gameObject));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthBar.Instance.Health -= 1;
        DOTween.Kill(this);
        Destroy(this.gameObject);
    }
}