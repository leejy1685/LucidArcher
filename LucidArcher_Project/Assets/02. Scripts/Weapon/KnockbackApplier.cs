using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackApplier : MonoBehaviour
{
    //�˹�
    float knockbackDuration; //�˹� �ð�
    Vector2 knockback;

    private void Update()
    {
        knockbackDuration -= Time.deltaTime;
    }

    public Vector2 ApplyKnockback(Vector2 direction)
    {
        // �˹� ���̸� �̵� �ӵ� ���� + �˹� ���� ����
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // �̵� �ӵ� ����
            direction += knockback;// �˹� ���� �߰�
        }

        return direction;
    }


    //�˹� ����
    public void Knockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        // ��� ������ �ݴ�� �о
        knockback = -(other.position - transform.position).normalized * power;
    }
}
