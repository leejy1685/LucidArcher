using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidHeart : ItemManager
{
    public  override void ItemAction(GameObject player)
    {
        PlayerStatHendler stat = player.GetComponent<PlayerStatHendler>();

        //루시드hp 증가로직
        stat.PlusLucidHP(1);
        
        Destroy(gameObject);

    }


}
