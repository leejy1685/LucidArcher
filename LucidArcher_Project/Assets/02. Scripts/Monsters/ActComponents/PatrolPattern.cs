using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPattern : MonoBehaviour
{
    MonsterBase monster;

    public Transform point1;
    public Transform point2;

    private Vector2 p1;
    private Vector2 p2;
    private Vector2 destination;

    private void Start()
    {
        p1 = monster.transform.position + point1.localPosition;
        p2 = monster.transform.position + point2.localPosition;
        destination = p1;
    }

    public void Init(MonsterBase monster)
    {
        this.monster = monster;
    }
    public void Move()
    {
        Vector2 posDiff = destination - (Vector2)monster.transform.position;
        
        if (posDiff.magnitude < monster.stats.MoveSpeed * Time.fixedDeltaTime)
        {
            monster.transform.position = destination;
            ToggleDestination();
        }
        else
        {
            monster.Move(posDiff.normalized);
        }
    }

    private void ToggleDestination()
    {
        destination = destination == p1 ? p2 : p1;
    }
}
