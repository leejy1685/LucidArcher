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
    
    public void Init(MonsterBase monster)
    {
        this.monster = monster;
        projectilePool = new Queue<MonsterProjectile>();

        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform);
            MonsterProjectile mProjectile = projectile.GetComponent<MonsterProjectile>();
            projectile.SetActive(false);
            projectilePool.Enqueue(mProjectile);
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
        if(projectilePool.Count <= 0) return false;

        MonsterProjectile projectile = projectilePool.Dequeue();
        projectile.Init(monster, projectilePool, transform.position, monster.detectedEnemy.transform.position);
        projectile.gameObject.SetActive(true);
        attackCooldown = monster.stats.AtkDelay;
        
        return true;
    }
}
