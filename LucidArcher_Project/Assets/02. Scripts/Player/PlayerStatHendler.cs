using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHendler : MonoBehaviour
{

    //�⺻ �̵� �ӵ�
    [Range(1, 20)][SerializeField] private float basespeed = 5f;
    //public float Speed { get { return basespeed; } set { basespeed = value; } }

    private float speedModifier = 0f;
    private bool isReversed = false;
    public float Speed => (basespeed + speedModifier) * (isReversed ? -1 : 1f);

    //ü��
    [Range(1, 10)][SerializeField] private int hp = 6;
    public int Hp { get { return hp; } set { hp = value; } }

    [Range(1, 10)][SerializeField] private int maxhp = 6;
    public int MaxHp { get { return maxhp; } set { maxhp = value; } }
    //��õ���Ʈ (�踮��)
    [Range(0, 3)][SerializeField] private int lucidhp = 0;
    public int LucidHp { get { return lucidhp; } set { lucidhp = value; } }
    //����
    [Range(1, 100)][SerializeField] private int level = 1;
    public int Level { get { return level; } set { level = value; } }
    //���¹̳� (�뽬 �� �� �� ���)
    [Range(1, 3)][SerializeField] private float stamina = 3;

    //�ִ� ���¹̳�
    public float Maxstamina = 3;
    public float Stamina
    {
        get => stamina;
        set => stamina = Mathf.Clamp(value, 0, 3);


    }
    // ���� ������
    [Range(0.1f, 1f)][SerializeField] private float attackDelay;
    public float AttackDelay { get { return attackDelay; } }

    //Ư��������
    [Range(0f, 100f)][SerializeField] private float lucidPower = 0f;
    public float LucidPower { get { return lucidPower; } set { lucidPower = value; } }
    //����ġ
    [Range(0, 20)][SerializeField] private int exp = 0;
    public int EXP { get { return exp; } set { exp = value; } }

    //�ִ� ����ġ
    public int MaxEXP => Level * 20;
    //�ִ�ü�� �� �� �������״��� Ȯ���ϱ� ���� ����
    public int UpgradeMaxHp_Count = 0;







    public void SetHP(int input)
    {
        Debug.Log($"HP{input}����");

        Hp += input;
    }


    

    public void UpgradeMaxHP() //�ִ�ü�� ���� �ִ� 4������ ������ ��Ʈ ��ĭ ����
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

    public void PlusLucidHP(int input) //�踮�� ����
    {
        Debug.Log($"��õ�HP{input}����");
        LucidHp += input;

    }

    public void PlusSpeed(float input) //���ǵ� ����
    {
        Debug.Log($"���ǵ�{input} ����");
        basespeed += input;
    }

    //public void ReverseMove() //���� ����
    //{

    //    basespeed *= -1;

    //}
    public void PlusLucidPower(float input) //Ư�� ������ ����
    {
        Debug.Log($"��õ� �Ŀ� {input}����");
        lucidPower += input;

    }

    public void PlusEXP(int input) //����ġ ����
    {

        Debug.Log($"����ġ {input}����");
        EXP += input;
    }
    public void LevelUP() //������
    {
        while (EXP >= MaxEXP)
        {
            EXP -= MaxEXP;
            Level++;
            Debug.Log($"������ ���� ����:{level}");

        }


    }
    public void RandSpeed(float speed, float duration)
    {

        StartCoroutine(RandomSlowBuff(speed, duration));

    }
    IEnumerator RandomSlowBuff(float speed, float duration)
    {
        Debug.Log("�̵��ӵ� ����");
        speedModifier -= speed;

        yield return new WaitForSeconds(duration);
        speedModifier += speed;
        Debug.Log("�̵��ӵ� ���ƿ�");




    }
    public void Reverse(float duration)
    {
        StartCoroutine(ReverseMoveBuff(duration));

    }
    IEnumerator ReverseMoveBuff(float duration)
    {

        Debug.Log("���� ����");
  

        isReversed = true;

        yield return new WaitForSeconds(duration);
        Debug.Log("���� ��������");
        isReversed = false;
    



    }

}
