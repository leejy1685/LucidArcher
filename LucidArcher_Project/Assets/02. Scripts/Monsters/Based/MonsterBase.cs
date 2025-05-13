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
    [SerializeField] public SpriteRenderer sprite;
    [SerializeField] protected Animator animator;
    [SerializeField] public Rigidbody2D rb;
    private MonsterSpawner monsterSpawner;

    public GameObject detectedEnemy;    // ���� ���忡�� enemy, �� �÷��̾�. Ȥ�� ����ƺ� ��ų�� ����ٸ� ����ƺ�, �����̻� �� ����


    //�˹�
    KnockbackApplier knockbackApplier;

    public event Action<float> OnTakeDamage;
    protected virtual void Start()
    {
        currentHP = stats.HP;


        //�˹�����
        knockbackApplier = GetComponent<KnockbackApplier>();

        if (sightCollider != null)
        {
            sightCollider.radius = stats.SightRange;
        }

    }   


    public void Init(MonsterSpawner _monsterSpawner)
    {
        monsterSpawner = _monsterSpawner;
        detectedEnemy = GameManager.Instance.player;
    }


    public void TakeDamage(float damage)
    {

        // TODO : HP ���
        float effectiveDamage = Mathf.Max(0.5f, damage - stats.Def); 
        currentHP -= effectiveDamage;
        if (currentHP < 0) { currentHP = 0; }

        float damagedDensity = Mathf.Min(effectiveDamage / 8, 1);
        sprite.color = Color.white - new Color(0, 1, 1, 0) * damagedDensity;
        
        if (currentHP <= 0) Die();

        OnTakeDamage?.Invoke(effectiveDamage);
    }

    protected virtual void Die()
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

    private void LateUpdate()
    {
        sprite.color = Color.Lerp(sprite.color, Color.white, 0.01f);
    }

    protected bool IsEnemyInRange()
    {
        if ((transform.position - detectedEnemy.transform.position).magnitude <= stats.Range) return true;
        return false;

    }
    public Vector2 GetDirectionTowardEnemy()
    {
        return (detectedEnemy.transform.position - transform.position).normalized;
    }

    public float GetDistanceToEnemy()
    {
        return (detectedEnemy.transform.position - transform.position).magnitude;
    }
}
