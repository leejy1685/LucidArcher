using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAtTargetPattern : MonoBehaviour
{
    MonsterBase monster;
    float attackCooldown = 0;
    public GameObject projectilePrefab;
    public int projectilePoolSize;
    private Queue<MonsterProjectile> projectilePool;
    public int emitterCountPerShot = 1;

    public void Init(MonsterBase monster)
    {
        this.monster = monster;
        projectilePool = new Queue<MonsterProjectile>();

        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, transform);
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

    public bool TryShot()
    {
        if (attackCooldown > 0) return false;
        if(projectilePool.Count < emitterCountPerShot) return false;

        for (int i = 0; i < emitterCountPerShot; i++)
        {
            MonsterProjectile projectile = projectilePool.Dequeue();
            projectile.Init(monster, projectilePool, transform.position, monster.detectedEnemy.transform.position);

            Vector2 shotDirection = monster.detectedEnemy.transform.position - transform.position;
            
            // rotZ : �ٶ󺸴� �������� ź ���� ����. ������ ���� ����� ������ źȯ��- �ϴ� ���� �� �����ôµ� ���� ���� �� �� �̴ϴ�
            // �Ƹ� �����տ��� źȯ�� �������� �ٶ󺸰� �ϸ� ���� �� �����ϴ�. ���� Ȯ�� �� �ּ� ���� ����
            float rotZ = Mathf.Atan2(shotDirection.y, shotDirection.x);
            rotZ = Mathf.Rad2Deg * rotZ;
            projectile.transform.rotation = Quaternion.Euler(0, 0, (360f / emitterCountPerShot) * i + rotZ);

            projectile.gameObject.SetActive(true);
            attackCooldown = monster.stats.AtkDelay;
        }
        
        return true;
    }
}
