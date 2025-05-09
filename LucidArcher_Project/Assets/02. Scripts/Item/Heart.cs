using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemManager
{
    public override void ItemAction(GameObject player)
    {
        //HP 증가로직
        //if(Player.Hp == Player.MaxHp) return;
        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();

        stat.PlusHP(1);
        Destroy(gameObject);

    }


}
