using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : ItemManager
{

    public override void ItemAction(GameObject player)
    {

        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();
        //����ġ �߰� ����


        stat.PlusEXP(1);

        Destroy(gameObject);

    }
}
