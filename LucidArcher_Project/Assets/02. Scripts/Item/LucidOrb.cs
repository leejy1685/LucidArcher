using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidOrb : ItemManager
{


    public override void ItemAction(GameObject player)
    {
        //Ư�������� ����
        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();

        //player.act ++;

        stat.PlusLucidPower(10f);
        Destroy(gameObject);

    }
}
