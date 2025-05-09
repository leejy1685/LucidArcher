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
    [Range(1, 3)][SerializeField] private float stamina = 3;
    public float Stamina { get { return stamina; } set { stamina = value; } }

    [Range(0.1f,1f)][SerializeField] private float attackDelay;
    public float AttackDelay { get { return attackDelay; } }


    [Range(0f,100f)][SerializeField] private float lucidPower = 0f;
    public float LucidPower { get { return lucidPower; } set { lucidPower = value; } }
   
    public void PlusHP()
    { 
        Hp++;
    }

    public void PlsySpeed()
    {

        Speed += 0.75f;
    }

    public void PlusLucidPower(float input)
    {
        lucidPower += input;

    }

    

}
