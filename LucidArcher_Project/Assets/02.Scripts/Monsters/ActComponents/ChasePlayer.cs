using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public MonsterBase chaser;

    internal void Chase(GameObject detectedEnemy)
    {
        Vector2 moveDirection = (detectedEnemy.transform.position - transform.position).normalized;
        chaser.Move(moveDirection);
    }
}
