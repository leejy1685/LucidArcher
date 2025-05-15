using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    //Player스텟
    private PlayerStatHendler stat;
    public PlayerStatHendler Stat { get { return stat; } }

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

    //대쉬기능 (일시 무적 및 속도 증가)
    private float dashTime = 0.5f;
    private float inDashTime;
    private bool isDash;

    //애니메이션
    Animator animator;

    //애니메이션 동작 관리
    const string MOVE = "IsMove";
    const string DASH = "IsDash";
    const string DAMAGE = "OnDamage";
    const string DIE = "IsDie";

    //대미지 무적 시간
    private float damageTime = 0.5f;
    private float onDamageTime;
    private bool onDamage;

    //루시드 파워
    private bool powerUp;
    public bool PowerUp { get { return powerUp; } }

    //장애물 레이어
    [SerializeField]LayerMask obstacle;

    //소리 추가
    [SerializeField] private AudioClip damageClip;
    [SerializeField] private AudioClip shildClip;
    [SerializeField] private AudioClip dashClip;

    private void Awake()
    {
        stat = GetComponent<PlayerStatHendler>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (weaponPrefap != null)
        {
            weaponController = Instantiate(weaponPrefap, weaponPivot);
            weaponController.Stat.ResetState();
        }

        animator = characterRenderer.GetComponent<Animator>();

        isDash = false;
        onDamage = false;
        powerUp = false;
    }


    private void Update()
    {
        //캐릭터 조작
        HandleAction();
    }

    private void FixedUpdate()
    {
        //게임 정지 시 조작 금지
        if (!GameManager.Instance.IsPlaying)
        {
            Movement(Vector2.zero);
            return;
        }

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
        CheckExtraTime();

        //사망처리
        PlayerDie();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((obstacle | 1 << collision.gameObject.layer) == obstacle)
            TakeDamage(1);
    }


    #region move and rotate

    //캐릭터 조작
    void HandleAction()
    {
        //상하좌우 입력
        float horizontal = KeyManager.getHorizontal();
        float vertical = KeyManager.getVertical();
        moveDirection = new Vector2(horizontal, vertical).normalized;

        //주변에 몬스터가 있을 때
        if (targets?.Length > 0 ) 
            lookDirection = NearestMonster().position - transform.position;
        else
            lookDirection = moveDirection * Stat.Speed;

        //대쉬중이 아니고, 이동 중 일 때, 대쉬 키를 누르면 대쉬
        if (!isDash  && Mathf.Abs(moveDirection.magnitude) > 0.5f && Stat.Stamina > 1
            && Input.GetKeyDown(KeyManager.keycode[(int)KeyInput.Dash]))
        {
            //스태미나 소모
            Stat.Stamina--;

            //일시 무적
            isDash = true;
            inDashTime = 0;

            //소리 추가
            SoundManager.PlayClip(dashClip);
        }
    }

    //이동
    void Movement(Vector2 direction)
    {
        //이동 방향에 속도 넣기
        direction = direction * (isDash?Stat.Speed*2:Stat.Speed);

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

        //근처에 몬스터가 있고 몬스터를 보고 있을 때
        if (targets.Length > 0 && lookDirection.magnitude > 0.5f)
        {
            //강화 상태 공격(공속 2배)
            if(powerUp && attackTime > Stat.AttackDelay / 2)
            {
                weaponController.CreateArrow(this, lookDirection, targetLayer);
                attackTime = 0;
            }
            //일반 공격 속도
            else if (attackTime > Stat.AttackDelay)
            {
                //루시드 파워업(공격 강화)
                lucidPowerUp();

                weaponController.CreateArrow(this, lookDirection, targetLayer);
                attackTime = 0;
            }
        }
    }

    //대쉬 기능, 유령화 기능
    void Dash()
    {
        //스태미나 회복
        Stat.Stamina += Time.deltaTime / 3;

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
        if (Stat.LucidHp > 0)
        {
            Stat.PlusLucidHP(-1);

            //소리 실행
            SoundManager.PlayClip(shildClip);
        }
        else
        {
            Stat.SetHP(-damage);

            //애니메이션
            animator.SetBool(DAMAGE, onDamage);

            //소리 실행
            SoundManager.PlayClip(damageClip);
        }
    }

    void CheckExtraTime()
    {
        //무적 시간 체크
        inDashTime += Time.deltaTime;
        onDamageTime += Time.deltaTime;

        //파워업 시간 체크
        if (powerUp)
        {
            Stat.LucidPower -= Time.deltaTime * 10;
        }

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

        //파워업 시간 종료
        if(Stat.LucidPower <= 0)
        {
            powerUp = false;
        }
    }


    //특수 기술
    void lucidPowerUp()
    {
        if(Stat.LucidPower > 100 && !powerUp)
        {
            powerUp = true;
        }
    }

    //플레이어 사망처리
    void PlayerDie()
    {
        if(Stat.Hp <= 0)
        {
            animator.SetBool(DIE, true);
            GameManager.Instance.GameOver();
        }
    }

    #endregion



}
