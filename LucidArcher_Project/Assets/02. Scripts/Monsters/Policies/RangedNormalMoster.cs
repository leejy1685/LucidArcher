using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNormalMoster : MonsterBase
{
    public enum State
    {
        Idle,
        Chase,
        Siege
    }
    [Header("Behaviour")]
    [SerializeField] ChasePattern chaseComponent;
    [SerializeField] ShotAtTargetPattern shotComponent;


    [SerializeField] State state;
    readonly int IsMove = Animator.StringToHash("IsMove");
    public float motionTime = 0.5f;
    private void Awake()
    {
        chaseComponent.Init(this);
        shotComponent.Init(this);

        state = State.Idle;
    }
    public override void OnPlayerDetected(GameObject Player)
    {
        base.OnPlayerDetected(Player);
        EnterChaseState();
    }

    public override void OnPlayerMissed()
    {
        base.OnPlayerMissed();
        chaseComponent.isChasing = false;

        if (state == State.Chase)
        {
            EnterIdleState();
        }

    }
    private void Update()
    {
        if (detectedEnemy != null)
        {          
            if (IsEnemyInRange() && shotComponent.CanShot())
            {
                EnterSiegeState();
            }

        }
    }
    #region EnterStates

    private void EnterIdleState()
    {
        rb.velocity = Vector3.zero;
        state = State.Idle;
        animator.SetBool(IsMove, false);
    }

    private void EnterChaseState()
    {
        if (detectedEnemy != null)
        {
            state = State.Chase;
            animator.SetBool(IsMove, true);
            chaseComponent.Execute();
        }
        else
        {
            EnterIdleState();
        }
    }
    private void EnterSiegeState()
    {
        chaseComponent.isChasing = false;
        state = State.Siege;
        rb.velocity = Vector3.zero;
        animator.SetBool(IsMove, false);
        shotComponent.Execute(EnterChaseState);
    }
    #endregion

}
