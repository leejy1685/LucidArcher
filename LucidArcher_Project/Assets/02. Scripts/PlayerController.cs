using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        if(weaponPrefap != null)
        {
            weaponController = Instantiate(weaponPrefap, weaponPivot);
        }

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

    #region move and rotate
    //캐릭터 이동 및 좌우반전
    private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    [Range(1, 20)][SerializeField] private float speed;

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

        if (weaponPivot != null)
        {
            // 무기 회전 처리
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        // 무기도 함께 좌우 반전 처리
        weaponController?.Rotate(isLeft);
    }

    //가장 가까운 몬스터의 위치를 리턴
    Vector2 NearestMonster()
    {
        //가장 작은 값
        GameObject target = null;
        float min = float.MaxValue;

        //조회
        foreach (GameObject monster in monsterList)
        {
            float distance = Vector3.Distance(monster.transform.position, transform.position);
            if (distance < min)
            {
                min = distance;
                target = monster;
            }
        }

        //리턴
        return target.transform.position;
    }



    #endregion

    //전투
    [SerializeField] private WeaponController weaponPrefap;
    private WeaponController weaponController;

    [SerializeField] List<GameObject> monsterList;
    [SerializeField] float attackDelay = 1;





}
