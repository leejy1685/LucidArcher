using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float maxHP;
    public float MaxHP => maxHP;
    float currentHP;

    [SerializeField] float def;
    public float DEF => def;

    [Header("Components")]
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Rigidbody2D rb;

    public GameObject detectedEnemy;


    void Start()
    {
        currentHP = MaxHP;
    }

    public void TakeDamage(float damage)
    {
        float effectiveDamage = Mathf.Max(0.5f, damage - def);
        currentHP -= effectiveDamage;

        float damagedDensity = Mathf.Min(effectiveDamage / 8, 1);
        sprite.color = Color.white - new Color(0, 1, 1, 0) * damagedDensity;

        if (currentHP <= 0) Die();
    }
    void Die()
    {
        // TODO
        gameObject.SetActive(false);
    }
}
