using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonsterBase
{
    [Header("Behaviour")]
    [SerializeField] ChasePlayer chaseComponent;

    int isMove = Animator.StringToHash("IsMove");
    private void Awake()
    {
        chaseComponent.chaser = this;
    }
    private void Update()
    {
        if(detectedEnemy != null)
        {
            animator.SetBool(isMove, true);
            chaseComponent.Chase(detectedEnemy);
        }
    }

    public override void OnPlayerMissed()
    {
        base.OnPlayerMissed();
        rb.velocity = Vector2.zero;
        animator.SetBool(isMove, false);
    }
}
