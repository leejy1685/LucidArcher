using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonsterBase
{
    enum State
    {
        Idle,
        Chase,
        Sprint
    }
    public enum Type
    {
        Normal,
        Sprinter
    }

    State state;

    [Header("Behaviour")]
    [SerializeField] ChasePattern chaseComponent;
    [SerializeField] SprintPattern sprintComponent;

    [Header("Properties")]
    public Type type;

    float remainedAttackDelay = 0;


    readonly int IsMove = Animator.StringToHash("IsMove");
    private void Awake()
    {
        chaseComponent.Init(this);
        if (type == Type.Sprinter) sprintComponent.Init(this);
    }

    private void Update()
    {
        if (remainedAttackDelay > 0)
        {
            remainedAttackDelay -= Time.deltaTime;
        }

        if (type == Type.Sprinter && state == State.Chase)
        {
            if (GetDistanceToEnemy() < stats.Range)
            {
                EnterSprintState();
            }
        }
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
            // 돌진이 끝나고보니 detectedEnemy가 없는 상황
            EnterIdleState();
        }
    }

    private void EnterSprintState()
    {
        if (remainedAttackDelay > 0) return;

        chaseComponent.StopChasing();
        remainedAttackDelay = stats.AtkDelay;
        state = State.Sprint;
        sprintComponent.Execute(EnterChaseState);
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(stats.Atk);
        }
    }
}
