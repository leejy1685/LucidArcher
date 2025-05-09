using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    [SerializeField] int bulletNum;    //ȭ�� ��
    public int BulletNum { get { return bulletNum; }set { damage = value; } }
    [SerializeField] float damage;    //�����
    public float Damage { get { return damage; } set { damage = value; } }
    [SerializeField] float bulletSpeed; //ȭ��ӵ�
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }

    //�˹� �ð��� �Ŀ�
    [SerializeField] float knockbackDuration = 0.5f;
    public float KnockbackDuration { get { return knockbackDuration; } }
    [SerializeField] float knockbackPower = 1f;
    public float KnockbackPower { get { return knockbackPower; } }

    public void PlusDamage(float input)
    {

        Damage += input;

    }
}
