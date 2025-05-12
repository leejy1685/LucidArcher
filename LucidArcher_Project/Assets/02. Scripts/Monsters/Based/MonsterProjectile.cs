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
    [SerializeField] float damage;

    [Header("Required Components")]
    [SerializeField] Rigidbody2D rb;

    [Header("Required Properties")]
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    float lifeRemained;
    [SerializeField] LayerMask targetMask;  // rigidbody ���̾� ���͸����� ����ϸ� ���� ����

    [Header("ExplodeEffect")]
    [SerializeField] GameObject effect;
    internal void Init(MonsterBase _owner, Queue<MonsterProjectile> projectilePool, Vector2 launchPosition, Vector2 _targetPosition)
    {
        owner = _owner;
        damage = owner.stats.Atk;
        basePool = projectilePool;
        transform.position = launchPosition;
        //transform.parent = null;    // ���߿� �ʿ��ϸ� ���ӸŴ����� ���ӸŴ����� ���� �Ŵ����� ���� ���̾��Ű���� �θ� �����ؼ� ����
        targetPosition = _targetPosition;
        direction = (targetPosition - launchPosition).normalized;
        lifeRemained = lifeTime;
        effect.SetActive(false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if((targetMask | 1 << collision.gameObject.layer) == targetMask)
        //{
        //    gameObject.SetActive(false);
        //}
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.contacts[0];
            effect.transform.parent = null;    // ���߿� �ʿ��ϸ� ���̾��Ű�� ������ �ʰ� ���ӸŴ����� ���� Ư�� ������Ʈ�� �Űܵ� ����
            effect.transform.localScale = Vector3.one;

            effect.transform.position = contact.point;
            effect.SetActive(true);
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
