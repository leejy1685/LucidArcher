using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    [SerializeField] int bulletNum;    //화살 수
    public int BulletNum { get { return bulletNum; }set { damage = value; } }
    [SerializeField] float damage;    //대미지
    public float Damage { get { return damage; } set { damage = value; } }
    [SerializeField] float bulletSpeed; //화살속도
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }

    //넉백 시간과 파워
    [SerializeField] float knockbackDuration = 0.5f;
    public float KnockbackDuration { get { return knockbackDuration; } }
    [SerializeField] float knockbackPower = 1f;
    public float KnockbackPower { get { return knockbackPower; } }

    public void PlusDamage(float input)
    {

        Damage += input;

    }
}
