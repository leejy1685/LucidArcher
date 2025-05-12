using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    //���� ����
    WeaponStat weaponStat;

    //�̵� ����
    private Rigidbody2D rigidbody2D;

    //���� ��ǥ
    LayerMask target;

    //��Ÿ� ���
    Vector2 shootPosition;
    float distance = 0;

    //�� Layer
    [SerializeField] LayerMask mapLayer;

    public void Init(LayerMask targetLayer,RangeWeaponController rangeWeaponController,Vector2 shootPosition)
    {
        //�̵� ����
        rigidbody2D = GetComponent<Rigidbody2D>();

        //���� ��ǥ
        target = targetLayer;

        //���� ����
        weaponStat = rangeWeaponController.Stat;

        //��Ÿ� ���
        this.shootPosition = shootPosition;
        distance = 0;
    }

    private void FixedUpdate()
    {
        rangeDestroy();
    }

    //ȭ�� ������
    public void ShootArrow(Vector2 lookDirection)
    {
        //���⿡ �ӵ� �ֱ�
        Vector2 velocity = lookDirection.normalized * weaponStat.BulletSpeed;
        rigidbody2D.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ÿ�ٰ� �浹
        if ((target | 1 << collision.gameObject.layer) == target)
        {
            //�˹� ����
            collision.GetComponent<KnockbackApplier>().Knockback(transform, weaponStat.KnockbackPower, weaponStat.KnockbackDuration);
            collision.GetComponent<MonsterBase>().TakeDamage(weaponStat.Damage);

            //�ı�
            Destroy(gameObject);
        }

        //�� �Ǵ� ��ֹ��� �浹
        if((mapLayer | 1 << collision.gameObject.layer) == mapLayer)
        {
            //�ı�
            Destroy(gameObject);
        }

    }

    //��Ÿ� ���
    private void rangeDestroy()
    {
        //�̵� �Ÿ� ���
        distance = Vector2.Distance(shootPosition, transform.position);

        //��Ÿ� ���� �ָ� ���� �ı�
        if (distance > weaponStat.Range)
        {
            Destroy(gameObject);
        }
    }

}
