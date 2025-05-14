using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FountainPattern : MonoBehaviour, IEnemyPattern
{
    public MonsterBase Monster { get; set; }
    public GameObject projectilePrefab;
    public int projectilePoolSize;
    private Queue<MonsterProjectile> projectilePool;

    [SerializeField] int emitterCountPerShot;
    [SerializeField] int shotCount;
    [SerializeField] float shotIntervalTime;

    

    public void Init(MonsterBase _monster)
    {
        Monster = _monster;
        projectilePool = new Queue<MonsterProjectile>();

        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, Monster.transform.parent);
            MonsterProjectile monsterProjectile = projectileGO.GetComponent<MonsterProjectile>();
            projectileGO.SetActive(false);
            projectilePool.Enqueue(monsterProjectile);
        }
    }
    public void Execute(Action enterStateAction)
    {
        if(projectilePool.Count >= emitterCountPerShot * shotCount) StartCoroutine(Fountain(enterStateAction));
        else enterStateAction.Invoke();
    }

    IEnumerator Fountain(Action enterStateAction)
    {
        float angleOffset = 360 / emitterCountPerShot / 2;
        for (int i = 0; i < shotCount; i++)
        {
            ShotFountain(angleOffset * i);
            yield return new WaitForSeconds(shotIntervalTime);
        }
        enterStateAction.Invoke();
    }

    private void ShotFountain(float angleOffset)
    {
        for (int i = 0; i < emitterCountPerShot; i++)
        {
            MonsterProjectile projectile = projectilePool.Dequeue();

            
            // 1. 각도 계산 (도 단위)
            float angleDeg = (360f / emitterCountPerShot) * i + angleOffset;
            float angleRad = angleDeg * Mathf.Deg2Rad;
            // 2. 방향 벡터 계산 (단위 벡터)
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
            projectile.Init(Monster, projectilePool, transform.position, (Vector2)transform.position + direction);

            projectile.transform.rotation = Quaternion.Euler(0, 0, angleDeg);

            projectile.gameObject.SetActive(true);

            
        }
    }
}
