using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public MonsterBase boss;
    public Slider slider;
    void Start()
    {
        slider.value = 1;
        boss.OnTakeDamage += UpdateHPbyDamage;
    }

    void UpdateHPbyDamage(float damage)
    {
        slider.value -= damage / boss.stats.HP;
    }
}
