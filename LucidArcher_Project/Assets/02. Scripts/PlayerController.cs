using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer characterRenderer;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    [Range(1, 20)][SerializeField] private float speed;

    LayerMask target;
    [SerializeField] List<GameObject> monsterList;

    [SerializeField] float attackDelay = 1;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleAction();
    }

    private void FixedUpdate()
    {
        Movement(moveDirection);
        Rotate(lookDirection);
        
    }


    void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;

        // 마우스 포인터의 스크린 좌표를 가져옴
        Vector2 targetPostion = NearestMonster();
        // 현재 위치와 마우스 위치 사이의 방향 벡터 계산
        lookDirection = (targetPostion - (Vector2)transform.position);
    }

    //이동
    void Movement(Vector2 direction)
    {
        direction = direction * speed;
        rigidbody2D.velocity = direction;
    }

    //캐릭터 좌우 반전
    void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        // 스프라이트 좌우 반전
        characterRenderer.flipX = isLeft;
    }

    Vector2 NearestMonster()
    {
        GameObject target = null;
        float min = float.MaxValue;
        foreach (GameObject monster in monsterList)
        {
            float distance = Vector3.Distance(monster.transform.position, transform.position);
            if (distance < min)
            {
                min = distance;
                target = monster;
            }
        }
        return target.transform.position;
    }

}
