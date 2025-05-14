using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePattern : MonoBehaviour, IEnemyPattern
{
    public MonsterBase Monster { get; set; }

    public bool isChasing;
   
    public void Init(MonsterBase monster)
    {
        Monster = monster;
        isChasing = false;
    }

    private void Update()
    {
        if (isChasing)
        {
            Vector2 moveDirection = (Monster.detectedEnemy.transform.position - transform.position).normalized;
            Monster.Move(moveDirection);
        }
    }
    public void Execute(Action enterStateAction = null)
    {
        isChasing = true;
    }

    public void StopChasing()
    {
        isChasing = false;
        Monster.rb.velocity = Vector2.zero;
    }

}
