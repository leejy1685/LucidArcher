using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAtTargetPattern : MonoBehaviour,IEnemyPattern
{
    public MonsterBase Monster { get; set; }
    float attackCooldown = 0;
    public GameObject projectilePrefab;
    public int projectilePoolSize;
    private Queue<MonsterProjectile> projectilePool;
    public int emitterCountPerShot = 1;

    public float shotIntervalTime;
    public float castingTime;

    public void Init(MonsterBase monster)
    {
        this.Monster = monster;
        projectilePool = new Queue<MonsterProjectile>();

        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject projectileGO = Instantiate(projectilePrefab);
            MonsterProjectile monsterProjectile = projectileGO.GetComponent<MonsterProjectile>();
            projectileGO.SetActive(false);
            projectilePool.Enqueue(monsterProjectile);
        }
    }

    private void Update()
    {
        if(! (attackCooldown < 0))
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public bool CanShot()
    {
        if (attackCooldown > 0) return false;
        if(projectilePool.Count < emitterCountPerShot) return false;
               
        return true;
    }

    public void Execute(Action enterStateAction)
    {
        StartCoroutine(Shot(enterStateAction));
    }

    IEnumerator Shot(Action enterStateAction)
    {
        for (int i = 0; i < emitterCountPerShot; i++)
        {
            MonsterProjectile projectile = projectilePool.Dequeue();
            
            projectile.Init(Monster, projectilePool, transform.position, Monster.detectedEnemy.transform.position);

            Vector2 shotDirection = Monster.detectedEnemy.transform.position - transform.position;

            // rotZ : �ٶ󺸴� �������� ź ���� ����. ������ ���� ����� ������ źȯ��- �ϴ� ���� �� �����ôµ� ���� ���� �� �� �̴ϴ�
            // �Ƹ� �����տ��� źȯ�� �������� �ٶ󺸰� �ϸ� ���� �� �����ϴ�. ���� Ȯ�� �� �ּ� ���� ����
            float rotZ = Mathf.Atan2(shotDirection.y, shotDirection.x);
            rotZ = Mathf.Rad2Deg * rotZ;
            projectile.transform.rotation = Quaternion.Euler(0, 0, (360f / emitterCountPerShot) * i + rotZ);

            projectile.gameObject.SetActive(true);
            attackCooldown = Monster.stats.AtkDelay;
            yield return new WaitForSeconds(shotIntervalTime);
        }
        yield return new WaitForSeconds(castingTime);
        enterStateAction.Invoke();
    }
}
