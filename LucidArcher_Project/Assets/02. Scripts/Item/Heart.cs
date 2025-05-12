using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemManager
{
    public override void ItemAction(GameObject player)
    {
        //HP ��������
        //if(Player.Hp == Player.MaxHp) return;
        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();
        if (stat.Hp == stat.MaxHp)
        {

            return;
        }

        if (stat.MaxHp - stat.Hp == 1)
        {
            stat.SetHP(1);
            Destroy(gameObject);
        }
        else
        {
            stat.SetHP(2);
            Destroy(gameObject);

        }


    }


}
