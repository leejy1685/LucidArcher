using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulHeart : ItemManager
{
    public  override void ItemAction(GameObject player)
    {
        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();

        //HP ��������

        //player.soulhp ++;
        Destroy(gameObject);

    }


}
