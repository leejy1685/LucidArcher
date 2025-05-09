using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    WeaponStat weaponStat;
    private Rigidbody2D rigidbody2D;
    LayerMask target;

    public void Init(LayerMask targetLayer,RangeWeaponController rangeWeaponController)
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        target = targetLayer;
        weaponStat = rangeWeaponController.Stat;
    }

    public void ShootArrow(Vector2 lookDirection,float speed)
    {
        Vector2 velocity = lookDirection.normalized * speed;
        rigidbody2D.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((target | 1 << collision.gameObject.layer) == target)
        {
            Destroy(gameObject);
            //³Ë¹é Àû¿ë
            collision.GetComponent<KnockbackApplier>().Knockback(transform, weaponStat.KnockbackPower, weaponStat.KnockbackDuration);
            collision.GetComponent<MonsterBase>().TakeDamage(weaponStat.Damage);

        }

    }

}
