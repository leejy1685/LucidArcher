using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalMoveOnlyMonster : MonsterBase
{
    Vector2 targetDirection;

    private void Awake()
    {
        int directionCase = Random.Range(0, 3);

        switch (directionCase)
        {
            case 0:
                targetDirection = new Vector2(-1, -1);
                break;
            case 1:
                targetDirection = new Vector2(-1, 1);
                break;
            case 2:
                targetDirection = new Vector2(-1, 1);
                break;
            case 3:
                targetDirection = new Vector2(1, 1);
                break;
        }
    }

    void Update()
    {
        Move(targetDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Vector2 normal = collision.contacts[0].normal;

            if(Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                targetDirection.x *= -1;
            }
            else
            {
                targetDirection.y *= -1;
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(stats.Atk);
        }
    }
}
