using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    //무기 성능
    WeaponStat weaponStat;

    //이동 구현
    private Rigidbody2D rigidbody2D;

    //공격 목표
    LayerMask target;

    //사거리 기능
    Vector2 shootPosition;
    float distance = 0;

    //맵 Layer
    [SerializeField] LayerMask mapLayer;

    public void Init(LayerMask targetLayer,RangeWeaponController rangeWeaponController,Vector2 shootPosition)
    {
        //이동 구현
        rigidbody2D = GetComponent<Rigidbody2D>();

        //공격 목표
        target = targetLayer;

        //무기 성능
        weaponStat = rangeWeaponController.Stat;

        //사거리 기능
        this.shootPosition = shootPosition;
        distance = 0;
    }

    private void FixedUpdate()
    {
        rangeDestroy();
    }

    //화살 날리기
    public void ShootArrow(Vector2 lookDirection)
    {
        //방향에 속도 넣기
        Vector2 velocity = lookDirection.normalized * weaponStat.BulletSpeed;
        rigidbody2D.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //타겟과 충돌
        if ((target | 1 << collision.gameObject.layer) == target)
        {
            //넉백 적용
            collision.GetComponent<KnockbackApplier>().Knockback(transform, weaponStat.KnockbackPower, weaponStat.KnockbackDuration);
            collision.GetComponent<MonsterBase>().TakeDamage(weaponStat.Damage);

            //파괴
            Destroy(gameObject);
        }

        //맵 또는 장애물과 충돌
        if((mapLayer | 1 << collision.gameObject.layer) == mapLayer)
        {
            //파괴
            Destroy(gameObject);
        }

    }

    //사거리 기능
    private void rangeDestroy()
    {
        //이동 거리 계산
        distance = Vector2.Distance(shootPosition, transform.position);

        //사거리 보다 멀리 가면 파괴
        if (distance > weaponStat.Range)
        {
            Destroy(gameObject);
        }
    }

}
