using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulHeart : ItemManager
{
    public  override void ItemAction(GameObject player)
    {
        //HP 증가로직
        Destroy(gameObject);

    }


}
