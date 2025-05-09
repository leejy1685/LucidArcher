using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidOrb : ItemManager
{


    public override void ItemAction(GameObject player)
    {
        //특수게이지 로직
        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();

        //player.act ++;

        stat.PlusLucidPower(10f);
        Destroy(gameObject);

    }
}
