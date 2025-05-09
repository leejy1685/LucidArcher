using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackApplier : MonoBehaviour
{
    //넉백
    float knockbackDuration; //넉백 시간
    Vector2 knockback;

    private void Update()
    {
        knockbackDuration -= Time.deltaTime;
    }

    public Vector2 ApplyKnockback(Vector2 direction)
    {
        // 넉백 중이면 이동 속도 감소 + 넉백 방향 적용
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // 이동 속도 감소
            direction += knockback;// 넉백 방향 추가
        }

        return direction;
    }


    //넉백 적용
    public void Knockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        // 상대 방향을 반대로 밀어냄
        knockback = -(other.position - transform.position).normalized * power;
    }
}
