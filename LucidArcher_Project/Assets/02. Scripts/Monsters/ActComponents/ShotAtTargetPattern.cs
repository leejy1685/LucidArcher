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
            
            // rotZ : 바라보는 방향으로 탄 각도 조절. 참격파 같은 모양이 나오는 탄환에- 일단 아직 안 만들어봤는데 방향 적용 잘 될 겁니다
            // 아마 프리팹에서 탄환이 오른쪽을 바라보게 하면 맞을 것 같습니다. 추후 확인 후 주석 수정 예정
            float rotZ = Mathf.Atan2(shotDirection.y, shotDirection.x);
            rotZ = Mathf.Rad2Deg * rotZ;
            projectile.transform.rotation = Quaternion.Euler(0, 0, (360f / emitterCountPerShot) * i + rotZ);

            projectile.gameObject.SetActive(true);
            attackCooldown = monster.stats.AtkDelay;
        }
        
        return true;
    }
}
