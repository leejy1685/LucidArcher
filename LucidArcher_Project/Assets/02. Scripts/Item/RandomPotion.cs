using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class RandomPotion : ItemManager
{
    public override void ItemAction(GameObject player)
    {
        // 랜덤 로직
        PlayerStatHendler Playerstat = player.GetComponent<PlayerStatHendler>();
        WeaponStat weaponstat = player.GetComponent<WeaponStat>();


        int random = Random.Range(0, 4);

        switch (random)

        {
            case 0:
                //체력회복
                Playerstat.PlusHP(1);
                break;

            case 1:
                // n초 동안 공격력증가
                StartCoroutine(RandomDamageBuff(weaponstat, 1, 7f));
                
                break;
            case 2:
                //데미지
                Playerstat.PlusHP(-1);
                break;
            case 3:
                //상태이상 (방향키 반전, 화상, 슬로우 등)
                Playerstat.PlusSpeed(-5f);
                break;


        }

        Destroy(gameObject);
    }

    private IEnumerator RandomDamageBuff(WeaponStat weaponstat, int damage, float duration)
    {
        weaponstat.PlusDamage(damage);
        yield return new WaitForSeconds(duration);
        weaponstat.PlusDamage(-damage);

    }
}
