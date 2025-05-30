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
    [SerializeField] int damage;

    [Header("Required Components")]
    [SerializeField] Rigidbody2D rb;

    [Header("Required Properties")]
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    float lifeRemained;
    [SerializeField] LayerMask targetMask;  // rigidbody 레이어 필터링으로 충분하면 지울 예정

    [Header("ExplodeEffect")]
    [SerializeField] GameObject effect;
    internal void Init(MonsterBase _owner, Queue<MonsterProjectile> projectilePool, Vector2 launchPosition, Vector2 _targetPosition)
    {
        owner = _owner;
        
        basePool = projectilePool;
        transform.position = launchPosition;
        //transform.parent = null;    // 나중에 필요하면 게임매니저나 게임매니저의 하위 매니저를 통해 하이어라키에서 부모 지정해서 정리
        targetPosition = _targetPosition;
        direction = (targetPosition - launchPosition).normalized;
        lifeRemained = lifeTime;
        effect.SetActive(false);
        Debug.DrawLine(launchPosition, targetPosition, Color.red, 2f);
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
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            ContactPoint2D contact = collision.contacts[0];
            ExplodeProjectile(contact);
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            ContactPoint2D contact = collision.contacts[0];
            ExplodeProjectile(contact);
        }
    }

    private void ExplodeProjectile(ContactPoint2D contact)
    {
        effect.transform.parent = null;    // 나중에 필요하면 하이어라키에 나돌지 않게 게임매니저를 통해 특정 오브젝트로 옮겨도 좋음
        effect.transform.localScale = Vector3.one;

        effect.transform.position = contact.point;
        effect.SetActive(true);

        //대미지 계산
        RetriveProjectile();
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
