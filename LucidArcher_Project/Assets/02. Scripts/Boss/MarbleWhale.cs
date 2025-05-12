using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleWhale : BossBase
{
    public enum State
    {
        Idle,
        Move,
        SprintAttack,
        FallingBubbleAttack
    }

    [SerializeField] State state;
    Vector2 moveDirection;
    [SerializeField] Location actArea;

    private void Start()
    {
        EnterIdleState();
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                // 할 거 없음. 다음 행동 선택할 코루틴 돌아가는 중
                break;
            case State.Move:
                HandleMoveState();  // 정황상 여기도 할 거 없음..
                break;
            case State.SprintAttack:
                HandleSprintAttackState();
                break;
            case State.FallingBubbleAttack:
                HandleFallingBubbleAttackState();
                break;
        }
    }

    private void HandleFallingBubbleAttackState()
    {
        throw new NotImplementedException();
    }

    private void HandleSprintAttackState()
    {
        throw new NotImplementedException();
    }

    private void HandleMoveState()
    {
        
    }
    

    IEnumerator EnterRandomPattern(float currentPatternDuration)
    {
        yield return new WaitForSeconds(currentPatternDuration);
        int rand = UnityEngine.Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                EnterIdleState();
                break;
            case 1:
                EnterMoveState();
                break;
        }
    }
    void EnterIdleState()
    {
        state = State.Idle;
        moveDirection = Vector2.zero;
        rb.velocity = Vector2.zero;
        float stateTimeRange = UnityEngine.Random.Range(1.5f, 3f);
        StartCoroutine(EnterRandomPattern(stateTimeRange));
    }
    private void EnterMoveState()
    {
        state = State.Move;
        moveDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        rb.velocity = moveDirection * 1.2f;

        sprite.flipX = moveDirection.x < 0 ? false : true;
        float stateTimeRange = UnityEngine.Random.Range(1.5f, 3f);
        StartCoroutine(EnterRandomPattern(stateTimeRange));
    }
}
