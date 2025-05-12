using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfHeart : ItemManager
{
    public override void ItemAction(GameObject player)
    {
        //HP ��������

        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();
        if (stat.Hp == stat.MaxHp) 
        {

            return;
        }


        stat.PlusHP(1);
        Destroy(gameObject);

    }


}
