using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePattern : MonoBehaviour, IEnemyPattern
{
    public MonsterBase Monster { get; set; }
    public GameObject waveObject;
    public float castingTime;
    public float attackThickness;

    float waveElapsedTime;

    
    public void Init(MonsterBase monster)
    {
        Monster = monster;
        waveObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        waveElapsedTime = float.MaxValue;
    }

    public void Execute(Action enterStateAction)
    {
        InitializeWave(); 
        StartCoroutine(WaveAttack(enterStateAction));
    }

    private void InitializeWave()
    {
        waveElapsedTime = 0;
        waveObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void FixedUpdate()
    {
        if (waveElapsedTime < castingTime)
        {
            waveElapsedTime += Time.fixedDeltaTime;

            float radiusMultiplier = Mathf.Pow(waveElapsedTime * 0.8f, 2.5f) * 15;
            waveObject.transform.localScale = Vector3.one * radiusMultiplier;

            float actualRadius = radiusMultiplier / 4;

            float enemyDistance = Monster.GetDistanceToEnemy();

            //Debug.Log("공격 반지름 : " + actualRadius + "  || 적군 거리 : " + enemyDistance);
            if (Mathf.Abs(actualRadius - enemyDistance) < attackThickness) 
                Monster.detectedEnemy.GetComponent<PlayerController>().TakeDamage(1);          
        }
    }

    IEnumerator WaveAttack(Action enterStateAction)
    {
        waveObject.SetActive(true);
        yield return new WaitForSeconds(castingTime);
        
        waveObject.SetActive(false);
        enterStateAction.Invoke();
    }
}
