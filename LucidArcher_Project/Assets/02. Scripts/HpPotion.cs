using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : ItemManager
{
    public  override void ItemAction(GameObject player)
    {
        //HP ��������
        Destroy(gameObject);

    }


}
