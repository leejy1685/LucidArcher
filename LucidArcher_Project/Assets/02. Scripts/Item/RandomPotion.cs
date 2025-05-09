using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class RandomPotion : ItemManager
{
    public override void ItemAction(GameObject player)
    {
        // ���� ����
        PlayerStatHendler Playerstat = player.GetComponent<PlayerStatHendler>();
        WeaponStat weaponstat = player.GetComponent<WeaponStat>();


        int random = Random.Range(0, 4);

        switch (random)

        {
            case 0:
                //ü��ȸ��
                Playerstat.PlusHP(1);
                break;

            case 1:
                // n�� ���� ���ݷ�����
                StartCoroutine(RandomDamageBuff(weaponstat, 1, 7f));
                
                break;
            case 2:
                //������
                Playerstat.PlusHP(-1);
                break;
            case 3:
                //�����̻� (����Ű ����, ȭ��, ���ο� ��)
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
