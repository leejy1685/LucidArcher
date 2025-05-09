using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class RandomPotion : ItemManager
{
    public override void ItemAction(GameObject player)
    {
        // 랜덤 로직

        int random = Random.Range(0, 4);

        switch (random)

        {
            case 0:
                //체력회복

                break;

            case 1:
                //공격력증가
                break;
            case 2:
                //데미지
                break;
            case 3:
                //상태이상 (방향키 반전, 화상, 슬로우 등)
                break;


        }

        Destroy(gameObject);
    }
}
