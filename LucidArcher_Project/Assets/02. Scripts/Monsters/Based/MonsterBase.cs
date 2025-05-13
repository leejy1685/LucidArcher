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

    public GameObject detectedEnemy;    // 쫒아가는 적, 여기서는 플레이어


    //넉백 적용
    KnockbackApplier knockbackApplier;

    protected virtual void Start()
    {
        currentHP = stats.HP;


        //넉백 컴포넌트
        knockbackApplier = GetComponent<KnockbackApplier>();

        if (sightCollider != null)
        {
            sightCollider.radius = stats.SightRange;
        }

    }   


    public void Init(MonsterSpawner _monsterSpawner)
    {
        monsterSpawner = _monsterSpawner;
    }


    public void TakeDamage(float damage)
    {

        // TODO : HP 소모 계산
        float effectiveDamage = Mathf.Max(0.5f, damage - stats.Def); 
        currentHP -= effectiveDamage;

        float damagedDensity = Mathf.Min(effectiveDamage / 8, 1);
        sprite.color = Color.white - new Color(0, 1, 1, 0) * damagedDensity;
        
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
        //넉백 적용
        direction = knockbackApplier.ApplyKnockback(direction);

        rb.velocity = direction;
    }

    private void LateUpdate()
    {
        sprite.color = Color.Lerp(sprite.color, Color.white, 0.01f);
    }

    protected bool IsEnemyInRange()
    {
        if ((transform.position - detectedEnemy.transform.position).magnitude <= stats.Range) return true;
        return false;

    }
}
