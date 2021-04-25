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
    private Sequence moveSequence;

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

        moveSequence = DOTween.Sequence();

        for (int i = 1; i < points.Count; i++)
        {
            MovePoint point = points[i];
            moveSequence.Append(rigidBody.DOMove(point.targetPoint, point.moveDuration).SetEase(Ease.Linear));
        }

        moveSequence.AppendCallback(() => Destroy(this.gameObject));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            HealthBar.Instance.Health -= 1;
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        moveSequence.Kill();
    }
}