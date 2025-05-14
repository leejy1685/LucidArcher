using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WanderPattern: MonoBehaviour, IEnemyPattern
{
    public MonsterBase Monster { get; set; }

    public bool isWandering;
    float remainedDirectionChangeTime;
    Vector2 moveDirection;


    public void Init(MonsterBase monster)
    {
        Monster = monster;
        isWandering = false;
    }

    private void Update()
    {
        if (isWandering)
        {
            remainedDirectionChangeTime -= Time.deltaTime;
            if (remainedDirectionChangeTime <= 0)
            {
                moveDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
                remainedDirectionChangeTime = UnityEngine.Random.Range(1f, 2f);
            }
            Monster.Move(moveDirection);
        }
    }
    public void Execute(Action actionAfterExecute = null)
    {
        isWandering = true;
    }


}
