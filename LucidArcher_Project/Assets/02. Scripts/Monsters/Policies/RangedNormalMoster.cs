using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNormalMoster : MonsterBase
{
    public enum State
    {
        Normal,
        Siege
    }
    [Header("Behaviour")]
    [SerializeField] ChasePattern chaseComponent;
    [SerializeField] ShotAtTargetPattern shotComponent;


    State state;
    readonly int IsMove = Animator.StringToHash("IsMove");
    public float motionTime = 0.5f;
    private void Awake()
    {
        chaseComponent.Init(this);
        shotComponent.Init(this);

        state = State.Normal;
    }

    private void Update()
    {
        if (detectedEnemy != null)
        {
            if (state == State.Normal)
            {
                animator.SetBool(IsMove, true);
                chaseComponent.Chase(detectedEnemy);

                if (IsEnemyInRange())
                {
                    TryShot();
                }
            }
            else if (state == State.Siege)
            {
                Move(Vector2.zero);
                
            }
        }
    }

    private void TryShot()
    {
        if (shotComponent.TryShot())
        {
            state = State.Siege;
            StartCoroutine(PlayShotMotion());
        }
    }

    IEnumerator PlayShotMotion()
    {
        yield return new WaitForSeconds(motionTime);
        state = State.Normal;
    }
}
