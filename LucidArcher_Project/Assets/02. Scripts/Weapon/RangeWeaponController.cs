using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RangeWeaponController : MonoBehaviour
{
    //무기의 성능
    WeaponStat stat;
    public WeaponStat Stat { get { return stat; } }
    //무기 이미지
    [SerializeField] SpriteRenderer weaponRenderer;
   
    //화살 프리펩
    [SerializeField] ArrowController arrow;

    //무기 애니메이션
    Animator animator;

    //애니메이션 동작 관리
    const string ATTACK = "IsAttack";
    

    private void Awake()
    {
        stat = GetComponent<WeaponStat>();
        animator = weaponRenderer.GetComponent<Animator>();
    }

    //무기 좌우 반전
    public void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }

    public void CreateArrow(Vector2 lookDirection,LayerMask targetLayer,float attackSpeed)
    {

        //각도 계산
        Quaternion quaternion = transform.rotation;
        quaternion = quaternion * Quaternion.Euler(0, 0, -90);

        //발사되는 포지션
        Vector2 shootPosition = transform.position;
        shootPosition.y += (stat.BulletNum - 1) * 0.1f; 

        //발사
        for (int i = 0; i < stat.BulletNum; i++)
        {
            ArrowController go = Instantiate(arrow, shootPosition, quaternion);
            go.Init(targetLayer,this,shootPosition);
            go.ShootArrow(lookDirection);

            //추가화살 포지션 조절
            shootPosition.y -= 0.2f;
        }

        //애니메이션
        animator.SetTrigger(ATTACK);
    }


}
