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
    float spriteInitialX;

    IEnemyPattern currentPattern;

    int nonPatternCount;
    private void Awake()
    {
        patterns = patternComponents.OfType<IEnemyPattern>().ToList();
        foreach (IEnemyPattern pattern in patterns)
        {
            pattern.Init(this);
        }
    }

    protected override void Start()
    {
        base.Start();
        EnterIdleState();
        spriteInitialX = sprite.gameObject.transform.localPosition.x;
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
                HandleMoveState();  // 정황상 여기도 할 거 없음..
                break;
            //case State.InPattern:
            //    HandlePatternState();
            //    break;
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
        FlipSpriteIfNeed();
        
        float stateTimeRange = UnityEngine.Random.Range(1.5f, 3f);
        StartCoroutine(EnterRandomPattern(stateTimeRange));
    }

    private void EnterInPatternState()
    {
        nonPatternCount = 0;
        rb.velocity = Vector2.zero;
        state = State.InPattern;
        moveDirection = GetDirectionTowardEnemy();
        FlipSpriteIfNeed();

        currentPattern = patterns[UnityEngine.Random.Range(0, patterns.Count)];
        currentPattern.Execute(EnterIdleState);
    }

    private void FlipSpriteIfNeed()
    {
        sprite.flipX = moveDirection.x < 0 ? false : true;
        Vector3 spritePos = sprite.gameObject.transform.localPosition;
        spritePos.x = moveDirection.x < 0 ? spriteInitialX : -spriteInitialX;
        sprite.gameObject.transform.localPosition = spritePos;
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
