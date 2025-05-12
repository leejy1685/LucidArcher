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


        int random = Random.Range(0, 5);

        switch (random)

        {
            case 0:
                //체력회복
                Debug.Log("체력 1회복");
                Playerstat.PlusHP(1);
                break;

            case 1:
                // n초 동안 공격력증가
                //StartCoroutine(RandomDamageBuff(weaponstat, weaponstat.Damage * 0.1f, 7f));
                weaponstat.RandBuff(weaponstat.Damage * 0.1f, 7f);

                break;
            case 2:
                //데미지
                Debug.Log("체력 1감소");
                Playerstat.PlusHP(-1);
                break;
            case 3:
                //상태이상 (슬로우)
                

                Playerstat.RandSpeed(7f, 7f);

                
                break;

            case 4:
                //상태이상 (이동반전)


                Playerstat.Reverse(7f);


                break;


        }

        Destroy(gameObject);
    }

    // IEnumerator RandomDamageBuff(WeaponStat weaponstat, float damage, float duration)
    //{
    //    Debug.Log($"{weaponstat.Damage}데미지에서 7초동안 10% 증가해서 {weaponstat.Damage * 1.1f}이 됐습니다. ");

    //    weaponstat.PlusDamage(damage);
    //    yield return new WaitForSeconds(duration);
    //    Debug.Log($"물약 효과가 종료되어 공격력이 원래 수치인{weaponstat.Damage}으로 됐습니다. ");

    //    weaponstat.PlusDamage(-damage);

    //}

    // IEnumerator RandomSlowBuff(PlayerStatHendler playerStat, float speed, float duration)
    //{

    //    playerStat.PlusSpeed(-speed);
    //    yield return new WaitForSeconds(duration);

    //    playerStat.PlusSpeed(speed);


    //}
}
