using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamCoin : ItemManager
{
    public int CoinValue = 1; //코인 먹으면 1원 증가하기 위한 변수
    public override void ItemAction(GameObject player)
    {
        //플레이어 코인 증가 로직
        Destroy(gameObject);

    }

}
