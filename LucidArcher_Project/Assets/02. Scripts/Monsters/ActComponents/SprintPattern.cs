using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SprintPattern : MonoBehaviour, IEnemyPattern
{
    MonsterBase monster;
    public float standByTime = 1f;

    [SerializeField] float sprintTime;

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
        
        Vector2 targetDirection = monster.GetDirectionTowardEnemy();
        monster.sprite.color = Color.cyan;
        yield return new WaitForSeconds(standByTime);
        monster.sprite.color = Color.white;
        monster.rb.velocity = targetDirection * 30f;
        yield return new WaitForSeconds(sprintTime);
        enterStateAction.Invoke();
    }
}
