using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidHeart : ItemManager
{
    public  override void ItemAction(GameObject player)
    {
        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();

        //��õ�hp ��������
        stat.PlusLucidHP(1);
        
        Destroy(gameObject);

    }


}
