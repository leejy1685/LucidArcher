using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerScript : MonoBehaviour
{
    //캐릭터 이동
    private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    //이동방향 보는 방향
    private Vector2 moveDirection;
    private Vector2 lookDirection;

    //이동 속도
    [Range(1, 20)][SerializeField] private float speed;

    //몬스터 인식
    public LayerMask targetLayer;
    RaycastHit2D[] targets;

    //인식 거리
    [SerializeField] float radius = 10f;

    //무기
    [SerializeField] private RangeWeaponController weaponPrefap;
    private RangeWeaponController weaponController;

    //공격 딜레이
    float attackTime;
    [SerializeField] float attackDelay = 1;

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
        // 원형 레이캐스트 시뮬레이션
        targets = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, 0, targetLayer);

        //이동과 캐릭터 전환
        Movement(moveDirection);
        Rotate(lookDirection);

        //공격
        attackTime += Time.deltaTime;
        Attack();
    }

    #region move and rotate

    //캐릭터 조작
    void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;

        if(targets.Length > 0 ) 
            lookDirection = NearestMonster().position - transform.position;
        else
            lookDirection = moveDirection;
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
    Transform NearestMonster()
    {
        Transform result = null;
        float diff = 100; //플레이어와의 거리

        //foreach 문으로 캐스팅 결와 오브젝트를 하나씩 방문
        foreach (RaycastHit2D target in targets)
        {  //CircleCastAll에 맞은 애들을 하나씩 접근
            Vector3 myPos = transform.position;             //플레이어 위치
            Vector3 targetPos = target.transform.position;  //타겟의 위치
            float curDiff = Vector3.Distance(myPos, targetPos); //거리를 구한다.

            if (curDiff < diff) //최소 거리를 저장
            {
                diff = curDiff;
                result = target.transform; //결과는 가장 가까운 타겟의 transform으로 지정
            }

        }

        return result;
    }



    #endregion


    #region Attack

    //공격 무기에서 화살 제작
    void Attack()
    {   //근처에 몬스터가 있을 때
        if (targets.Length > 0 && attackTime > attackDelay)
        {
            weaponController.CreateArrow(lookDirection, targetLayer);
            attackTime = 0;
        }
    }


    #endregion
}
