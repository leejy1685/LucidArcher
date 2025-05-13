using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleDetector : MonoBehaviour
{
    // 상수
    private static readonly WaitForSeconds WAIT_HALF_SEC = new WaitForSeconds(0.5f);

    // 변수
    private Dictionary<GameObject, bool> FallenObject = new Dictionary<GameObject, bool>();

    // 컴포넌트
    private Collider2D hole;

    private void Awake()
    {
        hole = GetComponent<Collider2D>();
    }

    // 떨어지는 애니메이션 효과
    IEnumerator Falling(Transform target)
    {
        Vector3 originPosition = target.position;
        Vector3 originScale = target.localScale;
        float timer = 0f;

        while(timer < 1f)
        {
            target.position = originPosition;
            target.localScale = Vector3.Lerp(originScale, Vector3.zero, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        target.position = originPosition + (originPosition - transform.position).normalized;
        target.localScale = originScale;
        FallenObject[target.gameObject] = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster") || collision.CompareTag("Player"))
        {
            Collider2D target = collision.GetComponent<Collider2D>();

            if (hole.bounds.Contains(target.bounds.min) && hole.bounds.Contains(target.bounds.max))
            {
                if (FallenObject.TryGetValue(target.gameObject, out bool isFall) && isFall)
                    return;

                FallenObject[target.gameObject] = true;
                StartCoroutine(Falling(target.transform));
            }
        }
    }
}
