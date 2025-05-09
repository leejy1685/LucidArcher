using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RangeWeaponController : MonoBehaviour
{
    //���� �̹���
    [SerializeField] SpriteRenderer weaponRenderer;

    //ȭ�� ��
    [SerializeField] int bulletNum;
    [SerializeField] int damage;
    [SerializeField] float bulletSpeed;

    [SerializeField] ArrowController arrow;

    public void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }

    public void CreateArrow(Vector2 lookDirection,LayerMask targetLayer)
    {
        //���� ���
        Quaternion quaternion = transform.rotation;
        quaternion = quaternion * Quaternion.Euler(0, 0, -90);

        //�߻�Ǵ� ������
        Vector2 shootPosition = transform.position;

        shootPosition.y += (bulletNum - 1) * 0.1f; 
        //�߻�
        for (int i = 0; i < bulletNum; i++)
        {
            ArrowController go = Instantiate(arrow, shootPosition, quaternion);
            go.Init(targetLayer);
            go.ShootArrow(lookDirection, bulletSpeed);

            //�߰�ȭ�� ������ ����
            shootPosition.y -= 0.2f;
        }
    }
}
