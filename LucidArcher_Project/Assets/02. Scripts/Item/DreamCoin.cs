using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamCoin : ItemManager
{
    public int CoinValue = 1; //���� ������ 1�� �����ϱ� ���� ����
    public override void ItemAction(GameObject player)
    {
        //�÷��̾� ���� ���� ����
        Destroy(gameObject);

    }

}
