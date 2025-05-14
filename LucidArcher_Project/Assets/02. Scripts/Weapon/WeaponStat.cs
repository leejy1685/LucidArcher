using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{

    [SerializeField] int bulletNum;    //화살개수
    public int BulletNum { get { return bulletNum; }set { bulletNum = value; } }
    [SerializeField] float damage;    //데미지
    public float Damage { get { return damage; } set { damage = value; } }
    [SerializeField] float bulletSpeed; //화살 속도
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }

    //넉백
    [SerializeField] float knockbackDuration = 0.5f;
    public float KnockbackDuration { get { return knockbackDuration; } }
    [SerializeField] float knockbackPower = 1f;
    public float KnockbackPower { get { return knockbackPower; } }


    //사거리
    [SerializeField] float range = 10f;
    public float Range { get { return range; } }

    public int UpgradeDamage_Count = 0;
    public int UpgradeBulletNum_Count = 0;


    public void ResetState()
    {
        BulletNum = 1;
        Damage = 100;
        UpgradeDamage_Count = 0;
        UpgradeBulletNum_Count = 0;
    }


    public void PlusDamage(float input) //데미지 증가
    {
        Damage += input;
    }


    public void UpgradeDamage() // 업그레이드 데미지 10% 증가 최대 4번
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


    public void UpgradeBulletNum() //업그레이드 화살 개수
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
        Debug.Log($"{Damage}데미지에서 10% 증가! {Damage * 1.1f}데미지가 됐습니다. ");
        float temp = Damage * 0.1f; //������ �̻��ϰ� �ȵǱ� ���ؼ�
        PlusDamage(damage);
        
        yield return new WaitForSeconds(duration);

        PlusDamage(-temp);
        Debug.Log($"버프가 끝나 {Damage}데미지로 돌아왔습니다. ");

    }


}
