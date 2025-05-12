using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapersHand : MonoBehaviour
{
    public LayerMask mask;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if((mask | 1<<collision.gameObject.layer) == mask)
        {
            MonsterBase enemy = collision.gameObject.GetComponent<MonsterBase>();
            //enemy.TakeDamage(25);
        }
    }
}
