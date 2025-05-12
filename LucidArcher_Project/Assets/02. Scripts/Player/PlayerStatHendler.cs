using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHendler : MonoBehaviour
{
    //�̵� �ӵ�
    [Range(1, 20)][SerializeField] private float speed;
    public float Speed {  get { return speed; } set { speed = value; } }
    //ü��
    [Range(1, 10)][SerializeField] private int hp = 6;
    public int Hp { get { return hp; } set { hp = value; } }
    //��õ���Ʈ (�踮��)
    [Range(0, 5)][SerializeField] private int lucidhp = 0;
    public int LucidHp { get { return lucidhp; } set { lucidhp = value; } }
    //����
    [Range(1, 100)][SerializeField] private int level = 1;
    public int Level { get { return level; } set { level = value; } }
    //���¹̳� (�뽬 �� �� �� ���)
    [Range(1, 3)][SerializeField] private float stamina = 3;
    public float Stamina { get { return stamina; } set { stamina = value; } }
    // ���� ������
    [Range(0.1f,1f)][SerializeField] private float attackDelay;
    public float AttackDelay { get { return attackDelay; } }

    //Ư��������
    [Range(0f,100f)][SerializeField] private float lucidPower = 0f;
    public float LucidPower { get { return lucidPower; } set { lucidPower = value; } }

    [Range(0, 20)][SerializeField] private int exp = 0;
    public int EXP { get { return exp; } set { exp = value; } }

    public void PlusHP(int input)
    {
        Debug.Log($"HP{input}����");

        Hp += input;
    }

    public void PlusLucidHP(int input)
    {
        Debug.Log($"��õ�HP{input}����");
        LucidHp += input;

    }

    public void PlusSpeed(float input)
    {
        Debug.Log($"���ǵ�{input} ����");
        Speed += input;
    }

    public void ReverseMove() //���� ����
    {

        Speed *= -1;

    }
    public void PlusLucidPower(float input)
    {
        Debug.Log($"��õ� �Ŀ� {input}����");
        lucidPower += input;

    }

    public void PlusEXP(int input)
    {

        Debug.Log($"����ġ {input}����");
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
        Debug.Log("�̵��ӵ� ����");
        PlusSpeed(-speed);
        yield return new WaitForSeconds(duration);
        Debug.Log("�̵��ӵ� ���ƿ�");
        PlusSpeed(speed);


    }
    public void Reverse(float duration)
    {
        StartCoroutine(ReverseMoveBuff(duration));

    }
    IEnumerator ReverseMoveBuff(float duration)
    {

        Debug.Log("���� ����");
        ReverseMove();
        yield return new WaitForSeconds(duration);
        Debug.Log("���� ��������");
        ReverseMove();



    }




}
