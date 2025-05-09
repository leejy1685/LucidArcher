using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public MonsterData stats;
    public float MaxHP => stats.HP;
    [SerializeField] protected float currentHP;

    [Header("Base")]
    [SerializeField] protected CircleCollider2D sightCollider;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rb;

    public GameObject detectedEnemy;    // 몬스터 입장에서 enemy, 즉 플레이어. 혹은 허수아비 스킬을 만든다면 허수아비, 상태이상 시 동족

    protected virtual void Start()
    {
        currentHP = stats.HP;
        if(sightCollider != null) sightCollider.radius = stats.SightRange;
    }   

    public void TakeDamage(float damage)
    {
        // TODO : HP 계산

        if (currentHP <= 0) Die();
    }

    void Die()
    {
        // TODO
    }

    public virtual void OnPlayerDetected(GameObject Player)
    {
        detectedEnemy = Player;
    }

    public virtual void OnPlayerMissed()
    {
        detectedEnemy = null;
    }

    public void Move(Vector2 direction)
    {
        sprite.flipX = direction.x < 0;
        rb.velocity = direction * stats.MoveSpeed;
    }

}
