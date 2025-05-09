using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemManager
{
    public  override void ItemAction(GameObject player)
    {
        //HP 증가로직
        Destroy(gameObject);

    }


}
