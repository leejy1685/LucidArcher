using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    //�÷��̾� ��Ʈ�ѷ�
    PlayerController playerController;

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

    //��ƼŬ �ý���
    [SerializeField] ParticleSystem impact;

    public void Init(PlayerController playerController, LayerMask targetLayer,RangeWeaponController rangeWeaponController,Vector2 shootPosition)
    {
        this.playerController = playerController;

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
        AttackEnemy(collision);
        ConflictMap(collision);
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

    private void AttackEnemy(Collider2D collision)
    {
        //Ÿ�ٰ� �浹
        if ((target | 1 << collision.gameObject.layer) == target)
        {
            //�˹� ����
            collision.GetComponent<KnockbackApplier>()?.Knockback(transform, weaponStat.KnockbackPower, weaponStat.KnockbackDuration);

            //����� ����
            if (playerController.PowerUp)
            {   //ĳ���� ��ȭ ����
                collision.GetComponent<MonsterBase>().TakeDamage(weaponStat.Damage*2);
            }
            else
            {   //��õ� ������ ��
                playerController.Stat.PlusLucidPower(5);
                collision.GetComponent<MonsterBase>().TakeDamage(weaponStat.Damage);
            }

            //�ı�
            Destroy(gameObject);
        }
    }

    private void ConflictMap(Collider2D collision)
    {
        //�� �Ǵ� ��ֹ��� �浹
        if ((mapLayer | 1 << collision.gameObject.layer) == mapLayer)
        {
            //�ı�
            Destroy(gameObject);
        }
    }

    //ȭ�� �ı��� ��ƼŬ �ý��� ����
    private void OnDestroy()
    {
        GameObject go = Instantiate(impact.gameObject,transform.position,Quaternion.identity);
        Destroy(go, 1f);
    }

}
