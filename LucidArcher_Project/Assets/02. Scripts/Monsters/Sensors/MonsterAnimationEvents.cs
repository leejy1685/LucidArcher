using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationEvents : MonoBehaviour
{
    public MonsterBase monster;

    // ������ ��ٷο� ��  MarbleWhale Die���� ������ �ð�����ؼ� �ذ�
    public void OnDeathAnimationEnd()
    {
        Destroy(monster.gameObject);
    }
}
