using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    [SerializeField] int bulletNum;    //ȭ�� ��
    public int BulletNum { get { return bulletNum; }set { bulletNum = value; } }
    [SerializeField] float damage;    //�����
    public float Damage { get { return damage; } set { damage = value; } }
    [SerializeField] float bulletSpeed; //ȭ��ӵ�
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }

    //�˹� �ð��� �Ŀ�
    [SerializeField] float knockbackDuration = 0.5f;
    public float KnockbackDuration { get { return knockbackDuration; } }
    [SerializeField] float knockbackPower = 1f;
    public float KnockbackPower { get { return knockbackPower; } }



    public int UpgradeDamage_Count = 0;
    public int UpgradeBulletNum_Count = 0;

    public void ResetState()
    {
        BulletNum = 1;
        Damage = 100;
        UpgradeDamage_Count = 0;
        UpgradeBulletNum_Count = 0;
    }

    public void PlusDamage(float input) //���ݷ�����
    {

        Damage += input;

    }

    public void UpgradeDamage() //���ݷ� ���׷��̵� 10%��
    {
        if (UpgradeDamage_Count < 4)
        {
            PlusDamage(Damage * 0.1f);
            UpgradeDamage_Count++;
        }
        else
        {

            return;
        }

    }


    public void UpgradeBulletNum() //ȭ�� ���� ����
    {
        if (UpgradeBulletNum_Count < 4)
        {
            BulletNum++;
            UpgradeBulletNum_Count++;

        }
        else
        {

            return;
        }

    }
    public void RandBuff(float damage, float duration)
    {

        StartCoroutine(RandomDamageBuff(damage,duration));

    }
    IEnumerator RandomDamageBuff( float damage, float duration)
    {
        Debug.Log($"{Damage}���������� 7�ʵ��� 10% �����ؼ� {Damage * 1.1f}�� �ƽ��ϴ�. ");
        float temp = Damage * 0.1f; //������ �̻��ϰ� �ȵǱ� ���ؼ�
        PlusDamage(damage);
        
        yield return new WaitForSeconds(duration);

        PlusDamage(-temp);
        Debug.Log($"���� ȿ���� ����Ǿ� ���ݷ��� ���� ��ġ��{Damage}���� �ƽ��ϴ�. ");

    }

    //���� ��Ÿ�
    [SerializeField] float range = 10f;
    public float Range { get { return range; } }


}
