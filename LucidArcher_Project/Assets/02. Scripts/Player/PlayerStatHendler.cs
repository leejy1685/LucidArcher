using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHendler : MonoBehaviour
{
    //�̵� �ӵ�
    [Range(1, 20)][SerializeField] private float speed;
    public float Speed {  get { return speed; } }
    //ü��
    [Range(1, 10)][SerializeField] private int hp = 6;
    public int Hp { get { return hp; } }
    [Range(1, 3)][SerializeField] private float stamina = 3;
    public float Stamina { get { return stamina; } }

    [Range(0.1f,1f)][SerializeField] private float attackDelay;
    public float AttackDelay { get { return attackDelay; } }
}
