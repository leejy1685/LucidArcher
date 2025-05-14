using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : ItemManager
{

    protected override void FixedUpdate()
    {
        if (Time.time - spawnTime < gravityDelay)
        {
            return;
        }

        if ((transform.position.y - itemY) < tolerance) //오차보다 낮을경우
        {

            rb.gravityScale = 0;
            rb.velocity = Vector2.zero; //멈춤

        }
        GameManager.Instance.AbsorbExp(this);
    }

    public override void ItemAction(GameObject player)
    {

        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();
        //경험치 추가 로직

        stat.PlusEXP(1);

        Destroy(gameObject);

    }


}
