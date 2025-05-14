using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWanderingMoster : MonsterBase
{
    public enum State
    {
        Wander,
        Siege
    }
    [Header("Behaviour")]
    [SerializeField] WanderPattern wanderComponent;
    [SerializeField] ShotAtTargetPattern shotComponent;


    [SerializeField] State state;
    readonly int IsMove = Animator.StringToHash("IsMove");

    float lastAttackTime;
    private void Awake()
    {
        wanderComponent.Init(this);
        shotComponent.Init(this);

        state = State.Wander;
        animator.SetBool(IsMove, true);
        lastAttackTime = float.MaxValue;
    }
    public override void OnPlayerDetected(GameObject Player)
    {
        base.OnPlayerDetected(Player);
        EnterWanderState();
    }

    public override void OnPlayerMissed()
    {
        base.OnPlayerMissed();
    }
    private void Update()
    {
        lastAttackTime += Time.deltaTime;
        if (detectedEnemy != null)
        {          
            if (IsEnemyInRange() && shotComponent.CanShot())
            {
                if(lastAttackTime > stats.AtkDelay) EnterSiegeState();
            }

        }
    }
    #region EnterStates

    private void EnterWanderState()
    {
        
        state = State.Wander;
        animator.SetBool(IsMove, true);
        wanderComponent.Execute();
    

    }
    private void EnterSiegeState()
    {
        lastAttackTime = 0;
        wanderComponent.isWandering = false;
        state = State.Siege;
        rb.velocity = Vector3.zero;
        animator.SetBool(IsMove, false);
        shotComponent.Execute(EnterWanderState);
    }
    #endregion

}
