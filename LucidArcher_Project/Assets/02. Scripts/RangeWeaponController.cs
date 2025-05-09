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

    public void ShotBullet(Vector2 lookDirection)
    {
        Quaternion quaternion = transform.rotation;
        quaternion = quaternion * Quaternion.Euler(0, 0, -90);
        ArrowController go = Instantiate(arrow,transform.position, quaternion);
        go.ShootBullet(lookDirection, bulletSpeed);
    }
}
