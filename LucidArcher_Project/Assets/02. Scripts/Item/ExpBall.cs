using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : ItemManager
{

    public override void ItemAction(GameObject player)
    {
        //����ġ �߰� ����
        Destroy(gameObject);

    }
}
