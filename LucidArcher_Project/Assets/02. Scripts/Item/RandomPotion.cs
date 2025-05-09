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
                Debug.Log("ü�� 1ȸ��");
                Playerstat.PlusHP(1);
                break;

            case 1:
                // n�� ���� ���ݷ�����
                //StartCoroutine(RandomDamageBuff(weaponstat, weaponstat.Damage * 0.1f, 1f));
                weaponstat.RandBuff(weaponstat.Damage * 0.1f, 1f);

                break;
            case 2:
                //������
                Debug.Log("ü�� 1����");
                Playerstat.PlusHP(-1);
                break;
            case 3:
                //�����̻� (����Ű ����, ȭ��, ���ο� ��)
                Debug.Log("�̵��ӵ� ����");

                StartCoroutine(RandomSlowBuff(Playerstat, 7f, 1f));

                
                break;


        }

        Destroy(gameObject);
    }

     IEnumerator RandomDamageBuff(WeaponStat weaponstat, float damage, float duration)
    {
        Debug.Log($"{weaponstat.Damage}���������� 7�ʵ��� 10% �����ؼ� {weaponstat.Damage * 1.1f}�� �ƽ��ϴ�. ");

        weaponstat.PlusDamage(damage);
        yield return new WaitForSeconds(duration);
        Debug.Log($"���� ȿ���� ����Ǿ� ���ݷ��� ���� ��ġ��{weaponstat.Damage}���� �ƽ��ϴ�. ");

        weaponstat.PlusDamage(-damage);

    }

     IEnumerator RandomSlowBuff(PlayerStatHendler playerStat, float speed, float duration)
    {

        playerStat.PlusSpeed(-speed);
        yield return new WaitForSeconds(duration);

        playerStat.PlusSpeed(speed);


    }
}
