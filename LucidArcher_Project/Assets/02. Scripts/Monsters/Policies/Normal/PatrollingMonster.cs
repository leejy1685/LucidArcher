using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingMonster : MonsterBase
{
    [Header("Behaviour")]
    [SerializeField] PatrolPattern patrolComponent;

    int isMove = Animator.StringToHash("IsMove");

    private void Awake()
    {
        patrolComponent.Init(this);
    }

    private void FixedUpdate()
    {
        patrolComponent.Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(stats.Atk);
        }
    }
}
