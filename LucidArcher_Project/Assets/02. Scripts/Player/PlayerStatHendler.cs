using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHendler : MonoBehaviour
{
    //�̵� �ӵ�
    [Range(1, 20)][SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
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
    public float Stamina { get { return stamina; } set { stamina = value; } }
    // ���� ������
    [Range(0.1f, 1f)][SerializeField] private float attackDelay;
    public float AttackDelay { get { return attackDelay; } set {attackDelay = value;} }

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
    public int UpgradeAttackDelay_Count = 0;
    public int UpgradePlayerSpeed_Count = 0;

    private bool hasAdditonalMaxHp = false;
    private bool hasAddionalLucidHp = false;

    public bool HasAdditionalMaxHp { get { return hasAdditonalMaxHp; } set { hasAdditonalMaxHp = value; } }
    public bool HasAddionalLucidHp { get { return hasAddionalLucidHp; } set { hasAddionalLucidHp = value; } }

    void Awake()
    {
        UpgradeMaxHp_Count = 0;
        UpgradeAttackDelay_Count = 0;
        UpgradePlayerSpeed_Count = 0;
    }

    public void SetHP(int input)
    {
        Debug.Log($"HP{input}����");

        Hp += input;
    }

    public void UpgradeMaxHP() //�ִ�ü�� ���� �ִ� 4������ ������ ��Ʈ ��ĭ ����
    {
        if (UpgradeMaxHp_Count < 4)
        {
            MaxHp += 1;
            Hp += 1;
            HasAdditionalMaxHp = true;
            UpgradeMaxHp_Count++;

        }
        else
        {

            return;
        }

    }

    public void UpgradeAttackDelay()
    {
        if (UpgradeAttackDelay_Count < 4)
        {
            AttackDelay -= 0.2f;
            UpgradeAttackDelay_Count++;

        }
        else
        {

            return;
        }
    }

    public void UpgradePlayerSpeed()
    {
        if (UpgradePlayerSpeed_Count < 4)
        {
            Speed += 0.75f;
            UpgradePlayerSpeed_Count++;

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
        HasAddionalLucidHp = true;

    }

    public void PlusSpeed(float input) //���ǵ� ����
    {
        Debug.Log($"���ǵ�{input} ����");
        Speed += input;
    }

    public void ReverseMove() //���� ����
    {

        Speed *= -1;

    }
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
        float temp = Speed;
        PlusSpeed(-speed);
        yield return new WaitForSeconds(duration);
        Debug.Log("�̵��ӵ� ���ƿ�");
        Speed = temp;


    }
    public void Reverse(float duration)
    {
        StartCoroutine(ReverseMoveBuff(duration));

    }
    IEnumerator ReverseMoveBuff(float duration)
    {

        Debug.Log("���� ����");
        float temp = Speed;

        ReverseMove();

        yield return new WaitForSeconds(duration);
        Debug.Log("���� ��������");
        Speed = temp;



    }




}
