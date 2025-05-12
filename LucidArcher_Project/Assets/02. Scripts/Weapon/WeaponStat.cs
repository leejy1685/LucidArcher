using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    [SerializeField] int bulletNum;    //ȭ�� ��
    public int BulletNum { get { return bulletNum; } }
    [SerializeField] float damage;    //�����
    public float Damage { get { return damage; } }
    [SerializeField] float bulletSpeed; //ȭ��ӵ�
    public float BulletSpeed { get { return bulletSpeed; } }

    //�˹� �ð��� �Ŀ�
    [SerializeField] float knockbackDuration = 0.5f;
    public float KnockbackDuration { get { return knockbackDuration; } }
    [SerializeField] float knockbackPower = 1f;
    public float KnockbackPower { get { return knockbackPower; } }

    [SerializeField] float range = 10f;
    public float Range { get { return range; } }

}
