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
    private MonsterSpawner monsterSpawner;

    public GameObject detectedEnemy;    // ���� ���忡�� enemy, �� �÷��̾�. Ȥ�� ����ƺ� ��ų�� ����ٸ� ����ƺ�, �����̻� �� ����


    //�˹�
    KnockbackApplier knockbackApplier;

    protected virtual void Start()
    {
        currentHP = stats.HP;
        if(sightCollider != null) sightCollider.radius = stats.SightRange;

        //�˹�����
        knockbackApplier = GetComponent<KnockbackApplier>();
    }   
}

    public void Init(MonsterSpawner _monsterSpawner)
    {
        monsterSpawner = _monsterSpawner;
    }


    public void TakeDamage(float damage)
    {
        // TODO : HP ���

        if (currentHP <= 0) Die();
    }

    void Die()
    {
        monsterSpawner.MonsterCount--;
        gameObject.SetActive(false);
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

        direction = direction* stats.MoveSpeed;
        //�˹� ����
        direction = knockbackApplier.ApplyKnockback(direction);

        rb.velocity = direction;
    }
}
