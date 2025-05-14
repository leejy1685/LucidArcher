using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarbleWhale : MonsterBase
{
    public enum State
    {
        Idle,
        Move,
        InPattern
    }

    readonly int IsDie = Animator.StringToHash("IsDie"); 

    [SerializeField] State state;
    Vector2 moveDirection;
    [SerializeField] Location actArea;
    [SerializeField] List<MonoBehaviour> patternComponents;
    List<IEnemyPattern> patterns;


    IEnemyPattern currentPattern;

    int nonPatternCount;
    private void Awake()
    {
        patterns = patternComponents.OfType<IEnemyPattern>().ToList();      
    }

    protected override void Start()
    {
        base.Start();
        EnterIdleState();
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                HandleIdleState();
                break;
            case State.Move:
                HandleMoveState();  
                break;
            //case State.InPattern:
            //    HandlePatternState();
            //    break;
        }
    }

    public override void Init(MonsterSpawner _monsterSpawner, Vector3 position)
    {
        base.Init(_monsterSpawner, position);
        detectedEnemy = GameManager.Instance.player.gameObject;
        actArea = transform.parent.GetComponent<Location>();

        foreach (IEnemyPattern pattern in patterns)
        {
            pattern.Init(this);

            // 패턴이 요구하는 세부 사항은 일단 이렇게 해둘게요. 
            // 개선 아이디어는 패턴 수만큼 필드 그냥 만들어서(어차피 보스 패턴은 셀 수 있을 만큼)
            // 인스펙터에서 리스트 아닌 걸로 받고 여기서 개별 초기화 후에 리스트로 묶는 거
            if (pattern is AirStrikePattern airStrike)
            {
                airStrike.SetLocation(actArea);
            }
        }
    }

    private void HandleIdleState()
    {
        rb.velocity = Vector2.zero;
    }

    private void HandleMoveState()
    {
        rb.velocity = moveDirection * 2f;
    }
    
    protected override void Die()
    {
        animator.SetTrigger(IsDie);
    }
    IEnumerator EnterRandomPattern(float currentPatternDuration)
    {
        
        yield return new WaitForSeconds(currentPatternDuration);
        if (nonPatternCount == 3)
        {
            EnterInPatternState();
            yield break;
        }
        nonPatternCount++;
        int rand = UnityEngine.Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                if (state == State.Idle) EnterMoveState();
                else EnterIdleState();
                break;
            case 1:
                EnterMoveState();
                break;
            case 2:
                EnterInPatternState();
                break;
        }
    }
    void EnterIdleState()
    {
        currentPattern = null;
        state = State.Idle;
        moveDirection = Vector2.zero;
        
        float stateTimeRange = UnityEngine.Random.Range(1f, 2f);
        StartCoroutine(EnterRandomPattern(stateTimeRange));
    }
    private void EnterMoveState()
    {
        state = State.Move;
        moveDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        FlipSprite();
        
        float stateTimeRange = UnityEngine.Random.Range(1.5f, 3f);
        StartCoroutine(EnterRandomPattern(stateTimeRange));
    }

    private void EnterInPatternState()
    {
        nonPatternCount = 0;
        rb.velocity = Vector2.zero;
        state = State.InPattern;
        moveDirection = GetDirectionTowardEnemy();
        FlipSprite();

        currentPattern = patterns[UnityEngine.Random.Range(0, patterns.Count)];
        currentPattern.Execute(EnterIdleState);
    }

    private void FlipSprite()
    {
        sprite.flipX = moveDirection.x < 0 ? false : true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            if(currentPattern is SprintPattern)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
