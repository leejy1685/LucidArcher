using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationEvents : MonoBehaviour
{
    public MonsterBase monster;

    // 적용이 까다로울 시  MarbleWhale Die에서 적당히 시간계산해서 해결
    public void OnDeathAnimationEnd()
    {
        monster.gameObject.SetActive(false);
        GameManager.Instance.monsterSpawner.DecreaseMonsterCount();
    }
}
