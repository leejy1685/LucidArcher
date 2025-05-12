using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHendler : MonoBehaviour
{
    //이동 속도
    [Range(1, 20)][SerializeField] private float speed;
    public float Speed {  get { return speed; } set { speed = value; } }
    //체력
    [Range(1, 10)][SerializeField] private int hp = 6;
    public int Hp { get { return hp; } set { hp = value; } }
    //루시드하트 (배리어)
    [Range(0, 5)][SerializeField] private int lucidhp = 0;
    public int LucidHp { get { return lucidhp; } set { lucidhp = value; } }
    //레벨
    [Range(1, 100)][SerializeField] private int level = 1;
    public int Level { get { return level; } set { level = value; } }
    //스태미너 (대쉬 등 쓸 떄 사용)
    [Range(1, 3)][SerializeField] private float stamina = 3;
    public float Stamina { get { return stamina; } set { stamina = value; } }
    // 공격 딜레이
    [Range(0.1f,1f)][SerializeField] private float attackDelay;
    public float AttackDelay { get { return attackDelay; } }

    //특수게이지
    [Range(0f,100f)][SerializeField] private float lucidPower = 0f;
    public float LucidPower { get { return lucidPower; } set { lucidPower = value; } }

    [Range(0, 20)][SerializeField] private int exp = 0;
    public int EXP { get { return exp; } set { exp = value; } }

    public void PlusHP(int input)
    {
        Debug.Log($"HP{input}증가");

        Hp += input;
    }

    public void PlusLucidHP(int input)
    {
        Debug.Log($"루시드HP{input}증가");
        LucidHp += input;

    }

    public void PlusSpeed(float input)
    {
        Debug.Log($"스피드{input} 증가");
        Speed += input;
    }

    public void ReverseMove() //조작 반전
    {

        Speed *= -1;

    }
    public void PlusLucidPower(float input)
    {
        Debug.Log($"루시드 파워 {input}증가");
        lucidPower += input;

    }

    public void PlusEXP(int input)
    {

        Debug.Log($"경험치 {input}증가");
        EXP += input;
    }
    public void LevelUP()
    {
        if(EXP >= 20)
        {
            int temp = EXP - 20;
            Level++;
            temp = EXP;


        }

    }
    public void RandSpeed(float speed, float duration)
    {

        StartCoroutine(RandomSlowBuff(speed, duration));

    }
    IEnumerator RandomSlowBuff(float speed, float duration)
    {
        Debug.Log("이동속도 감소");
        PlusSpeed(-speed);
        yield return new WaitForSeconds(duration);
        Debug.Log("이동속도 돌아옴");
        PlusSpeed(speed);


    }
    public void Reverse(float duration)
    {
        StartCoroutine(ReverseMoveBuff(duration));

    }
    IEnumerator ReverseMoveBuff(float duration)
    {

        Debug.Log("조작 반전");
        ReverseMove();
        yield return new WaitForSeconds(duration);
        Debug.Log("조작 반전해제");
        ReverseMove();



    }




}
