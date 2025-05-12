using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RangeWeaponController : MonoBehaviour
{
    //������ ����
    WeaponStat stat;
    public WeaponStat Stat { get { return stat; } }
    //���� �̹���
    [SerializeField] SpriteRenderer weaponRenderer;
   
    //ȭ�� ������
    [SerializeField] ArrowController arrow;

    //���� �ִϸ��̼�
    Animator animator;

    //�ִϸ��̼� ���� ����
    const string ATTACK = "IsAttack";
    

    private void Awake()
    {
        stat = GetComponent<WeaponStat>();
        animator = weaponRenderer.GetComponent<Animator>();
    }

    //���� �¿� ����
    public void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }

    public void CreateArrow(Vector2 lookDirection,LayerMask targetLayer,float attackSpeed)
    {

        //���� ���
        Quaternion quaternion = transform.rotation;
        quaternion = quaternion * Quaternion.Euler(0, 0, -90);

        //�߻�Ǵ� ������
        Vector2 shootPosition = transform.position;
        shootPosition.y += (stat.BulletNum - 1) * 0.1f; 

        //�߻�
        for (int i = 0; i < stat.BulletNum; i++)
        {
            ArrowController go = Instantiate(arrow, shootPosition, quaternion);
            go.Init(targetLayer,this,shootPosition);
            go.ShootArrow(lookDirection);

            //�߰�ȭ�� ������ ����
            shootPosition.y -= 0.2f;
        }

        //�ִϸ��̼�
        animator.SetTrigger(ATTACK);
    }


}
