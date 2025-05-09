using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonsterBase
{
    [Header("Behaviour")]
    [SerializeField] ChasePattern chaseComponent;

    readonly int IsMove = Animator.StringToHash("IsMove");
    private void Awake()
    {
        chaseComponent.Init(this);
    }
    private void Update()
    {
        if(detectedEnemy != null)
        {
            animator.SetBool(IsMove, true);
            chaseComponent.Chase(detectedEnemy);
        }
    }

    public override void OnPlayerMissed()
    {
        base.OnPlayerMissed();
        rb.velocity = Vector2.zero;
        animator.SetBool(IsMove, false);
    }
}
