using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingMonster : MonsterBase
{
    [Header("Behaviour")]
    [SerializeField] PatrolMovement patrolComponent;

    int isMove = Animator.StringToHash("IsMove");

    private void Awake()
    {
        patrolComponent.Init(this);
    }

    private void FixedUpdate()
    {
        patrolComponent.Move();
    }
}
