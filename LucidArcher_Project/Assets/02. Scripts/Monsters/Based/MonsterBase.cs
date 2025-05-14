using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

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
    float spriteOffsetX;

    public GameObject[] itemPrefabs; //아이템 목록 담을 리스트


    public float dropForce = 2f; //아이템 튀어오를때 힘


    protected virtual void Start()
    {
        spriteOffsetX = sprite.transform.localPosition.x;
        currentHP = stats.HP;


        //�˹�����
        knockbackApplier = GetComponent<KnockbackApplier>();

        if (sightCollider != null)
        {
            sightCollider.radius = stats.SightRange;
        }

    }   


    public void Init(MonsterSpawner _monsterSpawner, Vector3 position)
    {
        monsterSpawner = _monsterSpawner;
        transform.position = position;
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
        DropItems();

        gameObject.SetActive(false);
        monsterSpawner.DecreaseMonsterCount();
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

        Vector3 spritePos = sprite.transform.localPosition;
        spritePos.x *= direction.x < 0 ? -spriteOffsetX : spriteOffsetX;
        sprite.transform.localPosition = spritePos;

        direction = direction* stats.MoveSpeed;
        //�˹� ����
        //direction = knockbackApplier.ApplyKnockback(direction);

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

    void DropItems()
    {

        int itemcount = Random.Range(1, 6); //드랍 아이템 개수

        for (int i = 0; i < itemcount; i++) // 드랍 아이템 수 만큼 반복
        {
            GameObject item_prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)]; //아이템 목록중에 하나 정함

            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.15f, 0.15f), -0.2f, 0); //아이템 나올 위치 랜덤으로 설정

            GameObject item = Instantiate(item_prefab, spawnPos, Quaternion.identity); //아이템 생성

            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null) //rigidbody 존재시
            {
                // 위로 + 옆으로 약간 튀는 랜덤 힘
                Vector2 force = new Vector2(Random.Range(-1f, 1f), 1f) * dropForce;
                rb.AddForce(force, ForceMode2D.Impulse);
            }

            ItemManager drop = item.GetComponent<ItemManager>();
            if (drop != null)
            {

                drop.itemY = transform.position.y;
            }

        }



    }
}
