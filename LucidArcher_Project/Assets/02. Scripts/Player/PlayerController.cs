using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    //Player스텟
    PlayerStatHendler stat;

    //캐릭터 이동
    private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    //이동방향 보는 방향
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 lookDirection = Vector2.zero;

    //몬스터 인식
    public LayerMask targetLayer;
    RaycastHit2D[] targets;

    //인식 거리
    [SerializeField] float radius;

    //무기
    [SerializeField] private RangeWeaponController weaponPrefap;
    private RangeWeaponController weaponController;

    //공격 속도
    private float attackTime;

    //대쉬기능 (일시 무적)
    private float dashTime = 1f;
    private float inDashTime;
    private bool isDash;

    //애니메이션
    Animator animator;

    //애니메이션 동작 관리
    const string MOVE = "IsMove";
    const string DASH = "IsDash";
    const string DAMAGE = "OnDamage";

    //대미지 무적 시간
    private float damageTime = 0.5f;
    private float onDamageTime;
    private bool onDamage;

    //방어막
    [SerializeField] int shield;
    public int Shield
    {
        get => shield;
        set => shield = Mathf.Clamp(value, 0, 3);
    }


    private void Awake()
    {
        stat = GetComponent<PlayerStatHendler>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (weaponPrefap != null)
        {
            weaponController = Instantiate(weaponPrefap, weaponPivot);
        }

        animator = characterRenderer.GetComponent<Animator>();
    }


    private void Update()
    {
        //캐릭터 조작
        HandleAction();
    }

    private void FixedUpdate()
    {
        // 원형 레이캐스트로 몬스터 탐색
        targets = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, 0, targetLayer);

        //이동과 캐릭터 전환
        Movement(moveDirection);
        Rotate(lookDirection);
        
        //공격
        Attack();

        //대쉬
        Dash();

        //무적 시간 확인
        CheckSuperTime();
    }

    #region move and rotate

    //캐릭터 조작
    void HandleAction()
    {
        //상하좌우 입력
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;

        //주변에 몬스터가 있을 때
        if(targets.Length > 0 ) 
            lookDirection = NearestMonster().position - transform.position;
        else
            lookDirection = moveDirection;

        //대쉬중이 아니고, 이동 중 일 때, 스패이스를 누르면 대쉬
        if (!isDash && Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(moveDirection.magnitude) > 0.5f)
        {
            isDash = true;
            inDashTime = 0;
        }
    }

    //이동
    void Movement(Vector2 direction)
    {
        //이동 방향에 속도 넣기
        direction = direction * stat.Speed;

        //물리 실행
        rigidbody2D.velocity = direction;

        //애니메이션
        animator.SetBool(MOVE, direction.magnitude > 0.5f);
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
        {  
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

    #region Battle

    //공격 무기에서 화살 제작
    void Attack()
    {
        //공격 속도 계산
        attackTime += Time.deltaTime;
        //근처에 몬스터가 있을 때
        if (targets.Length > 0 && attackTime > stat.AttackDelay)
        {
            weaponController.CreateArrow(lookDirection, targetLayer,stat.AttackDelay);
            attackTime = 0;
        }
    }

    //대쉬 기능, 유령화 기능
    void Dash()
    {
        //스태미나 소모
        //stat.Stamina--;

        //충돌무시
        int targetLayerIndex = (int)Mathf.Log(targetLayer.value, 2);
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, targetLayerIndex, isDash);

        //애니메이션
        animator.SetBool(DASH, isDash);
    }

    public void TakeDamage(int damage)
    {
        //대쉬 또는 무적시간 중 대미지를 받지 않음
        if (isDash || onDamage)
            return;

        //일시 무적
        onDamage = true;
        onDamageTime = 0;

        //대미지 계산
        if (shield > 0)
        {
            shield--;
            Debug.Log("방어막 소모");
        }
        else
        {
            stat.SetHP(-damage);
            Debug.Log("체력 소모 " + damage);

            //애니메이션
            animator.SetBool(DAMAGE, onDamage);
        }
    }

    void CheckSuperTime()
    {
        //무적 시간 체크
        inDashTime += Time.deltaTime;
        onDamageTime += Time.deltaTime;

        //무적 종료
        if (inDashTime > dashTime)
        {
            isDash = false;
        }

        if(onDamageTime > damageTime)
        {
            onDamage = false;
            animator.SetBool(DAMAGE, onDamage);
        }
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(2);
    }


}
