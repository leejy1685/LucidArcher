using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    MonsterBase owner;
    Queue<MonsterProjectile> basePool;

    [SerializeField] Vector2 targetPosition;
    [SerializeField] Vector2 direction;

    [Header("Required Components")]
    [SerializeField] Rigidbody2D rb;

    [Header("Properties")]
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float lifeTime;
    float lifeRemained;
    [SerializeField] LayerMask targetMask;
    internal void Init(MonsterBase _owner, Queue<MonsterProjectile> projectilePool, Vector2 launchPosition, Vector2 _targetPosition)
    {
        owner = _owner;
        basePool = projectilePool;
        transform.position = launchPosition;
        targetPosition = _targetPosition;
        direction = (targetPosition - launchPosition).normalized;
        lifeRemained = lifeTime;
    }

    private void Update()
    {
        rb.velocity = direction * speed;

        lifeRemained -= Time.deltaTime;
        if (lifeRemained < 0)
        {
            RetriveProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if((targetMask | 1 << collision.gameObject.layer) == targetMask)
        //{
        //    gameObject.SetActive(false);
        //}
        if (collision.CompareTag("Player"))
        {
            RetriveProjectile();
        }
    }

    void RetriveProjectile()
    {
        if (owner == null) Destroy(gameObject);
        else
        {
            basePool.Enqueue(this);
            gameObject.SetActive(false);
        }
    }
}
