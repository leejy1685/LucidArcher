using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SprintPattern : MonoBehaviour, IEnemyPattern
{
    MonsterBase monster;
    float standByTime = 1.5f;

    public MonsterBase Monster { get; set; }

    public void Execute(Action enterStateAction)
    {
        StartCoroutine(Sprint(enterStateAction));
    }

    public void Init(MonsterBase monster)
    {
        this.monster = monster;
    }

    IEnumerator Sprint(Action enterStateAction)
    {
        yield return new WaitForSeconds(standByTime/3);
        Vector2 targetDirection = monster.GetDirectionTowardEnemy();
        monster.sprite.color = Color.cyan;
        yield return new WaitForSeconds(standByTime/3*2);
        monster.sprite.color = Color.white;
        monster.rb.velocity = targetDirection * 30f;
        yield return new WaitForSeconds(2f);
        enterStateAction.Invoke();
    }
}
