using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RangeWeaponController : MonoBehaviour
{
    //무기 이미지
    [SerializeField] SpriteRenderer weaponRenderer;

    //화살 수
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
        //각도 계산
        Quaternion quaternion = transform.rotation;
        quaternion = quaternion * Quaternion.Euler(0, 0, -90);

        //발사되는 포지션
        Vector2 shootPosition = transform.position;

        shootPosition.y += (bulletNum - 1) * 0.1f; 
        //발사
        for (int i = 0; i < bulletNum; i++)
        {
            ArrowController go = Instantiate(arrow, shootPosition, quaternion);
            go.Init(targetLayer);
            go.ShootArrow(lookDirection, bulletSpeed);

            //추가화살 포지션 조절
            shootPosition.y -= 0.2f;
        }
    }
}
