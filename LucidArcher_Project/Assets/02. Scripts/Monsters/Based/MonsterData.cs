using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObject/Monster Data")]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private string monsterName;
    public string MonsterName => MonsterName;

    [SerializeField]
    private float hp;
    public float HP => hp;

    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    [SerializeField]
    private int atk;
    public int Atk => atk;

    [SerializeField]
    private float atkDelay;
    public float AtkDelay => atkDelay;

    [SerializeField]
    private float def;
    public float Def => def;

    [SerializeField]
    private float range;
    public float Range => range;

    [SerializeField]
    private float sightRange;
    public float SightRange => sightRange;
}
