using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePattern : MonoBehaviour
{
    private MonsterBase chaser;

    public void Init(MonsterBase monster)
    {
        chaser = monster;
    }
    internal void Chase(GameObject detectedEnemy)
    {
        Vector2 moveDirection = (detectedEnemy.transform.position - transform.position).normalized;
        chaser.Move(moveDirection);
    }
}
