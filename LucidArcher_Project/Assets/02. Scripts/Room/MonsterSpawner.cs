using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 외부 오브젝트
    private RoomHandler room;
    private List<MonsterBase> monsters = new List<MonsterBase>();

    // 프리팹
    [SerializeField] private GameObject[] monsterPrefabs;

    // 변수
    private int monsterCount = 1;
    public int MonsterCount
    {
        get { return monsterCount; }
        set
        {
            if (value <= 0)
            {
                monsterCount = 0;
                room.EndEvent();
            }
            else monsterCount = value;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
            DestroyAllMonsters();
    }

    // 몬스터 스포너 초기화
    public void Init(RoomHandler _room, int _monsterCount)
    {
        room = _room;
        monsterCount = _monsterCount;
    }

    // 몬스터 소환
    public void SpawnMosnters()
    {
        for (int i = 0; i < monsterCount; i++)
        {
            GameObject monster = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], transform);
            monster.transform.localPosition = RandomPosition();
            monsters.Add(monster.GetComponent<MonsterBase>());
        }
    }

    // 랜덤 포지션 반환
    private Vector3 RandomPosition()
    {
        Vector2 colliderSize = room.GetComponent<BoxCollider2D>().size;

        float x = Random.Range(-colliderSize.x * 0.5f, colliderSize.x * 0.5f);
        float y = Random.Range(-colliderSize.y * 0.5f, colliderSize.y * 0.5f);

        return new Vector3(x, y, 0);
    }

    // 테스트용 : 모든 몬스터 삭제
    private void DestroyAllMonsters()
    {
        if (monsterCount == 0) return;

        foreach(MonsterBase monster in monsters)
        {
            monster.gameObject.SetActive(false);
        }
        MonsterCount = 0;
    }
}
