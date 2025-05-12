using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHendler : MonoBehaviour
{
    //이동 속도
    [Range(1, 20)][SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    //체력
    [Range(1, 10)][SerializeField] private int hp = 6;
    public int Hp { get { return hp; } set { hp = value; } }

    [Range(1, 10)][SerializeField] private int maxhp = 6;
    public int MaxHp { get { return maxhp; } set { maxhp = value; } }
    //루시드하트 (배리어)
    [Range(0, 3)][SerializeField] private int lucidhp = 0;
    public int LucidHp { get { return lucidhp; } set { lucidhp = value; } }
    //레벨
    [Range(1, 100)][SerializeField] private int level = 1;
    public int Level { get { return level; } set { level = value; } }
    //스태미너 (대쉬 등 쓸 떄 사용)
    [Range(1, 3)][SerializeField] private float stamina = 3;

    //최대 스태미너
    public float Maxstamina = 3;
    public float Stamina { get { return stamina; } set { stamina = value; } }
    // 공격 딜레이
    [Range(0.1f, 1f)][SerializeField] private float attackDelay;
    public float AttackDelay { get { return attackDelay; } }

    //특수게이지
    [Range(0f, 100f)][SerializeField] private float lucidPower = 0f;
    public float LucidPower { get { return lucidPower; } set { lucidPower = value; } }
    //경험치
    [Range(0, 20)][SerializeField] private int exp = 0;
    public int EXP { get { return exp; } set { exp = value; } }

    //최대 경험치
    public int MaxEXP => Level * 20;
    //최대체력 몇 번 증가시켰는지 확인하기 위한 변수
    public int UpgradeMaxHp_Count = 0;

    public void SetHP(int input)
    {
        Debug.Log($"HP{input}증가");

        Hp += input;
    }

    public void UpgradeMaxHP() //최대체력 증가 최대 4번까지 증가시 하트 한칸 증가
    {
        if (UpgradeMaxHp_Count < 4)
        {
            MaxHp += 2;
            Hp += 2;
            UpgradeMaxHp_Count++;

        }
        else
        {

            return;
        }

    }

    public void PlusLucidHP(int input) //배리어 증가
    {
        Debug.Log($"루시드HP{input}증가");
        LucidHp += input;

    }

    public void PlusSpeed(float input) //스피드 증가
    {
        Debug.Log($"스피드{input} 증가");
        Speed += input;
    }

    public void ReverseMove() //조작 반전
    {

        Speed *= -1;

    }
    public void PlusLucidPower(float input) //특수 게이지 증가
    {
        Debug.Log($"루시드 파워 {input}증가");
        lucidPower += input;

    }

    public void PlusEXP(int input) //경험치 증가
    {

        Debug.Log($"경험치 {input}증가");
        EXP += input;
    }
    public void LevelUP() //레벨업
    {
        while (EXP >= MaxEXP)
        {
            EXP -= MaxEXP;
            Level++;
            Debug.Log($"레벨업 현재 레벨:{level}");

        }


    }
    public void RandSpeed(float speed, float duration)
    {

        StartCoroutine(RandomSlowBuff(speed, duration));

    }
    IEnumerator RandomSlowBuff(float speed, float duration)
    {
        Debug.Log("이동속도 감소");
        float temp = Speed;
        PlusSpeed(-speed);
        yield return new WaitForSeconds(duration);
        Debug.Log("이동속도 돌아옴");
        Speed = temp;


    }
    public void Reverse(float duration)
    {
        StartCoroutine(ReverseMoveBuff(duration));

    }
    IEnumerator ReverseMoveBuff(float duration)
    {

        Debug.Log("조작 반전");
        //float temp = Speed;

        ReverseMove();

        yield return new WaitForSeconds(duration);
        Debug.Log("조작 반전해제");
        ReverseMove();
        //Speed = temp;



    }

}
