using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidOrb : ItemManager
{


    public override void ItemAction(GameObject player)
    {
        //Ư�������� ����
        PlayerStatHendler Playerstat = player.GetComponent<PlayerStatHendler>();

        //player.act ++;

        Playerstat.PlusLucidPower(10f);
        Destroy(gameObject);

    }
}
