using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulHeart : ItemManager
{
    public  override void ItemAction(GameObject player)
    {
        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();

        //HP 증가로직

        //player.soulhp ++;
        Destroy(gameObject);

    }


}
