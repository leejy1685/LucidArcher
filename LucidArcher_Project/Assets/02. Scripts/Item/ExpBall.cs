using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : ItemManager
{

    public override void ItemAction(GameObject player)
    {

        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();
        //경험치 추가 로직


        stat.PlusEXP(1);

        Destroy(gameObject);

    }
}
