using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 외부 오브젝트
    private RoomHandler room;
    private List<MonsterBase> monsters = new List<MonsterBase>();

    // 프리팹
    [SerializeField] private GameObject[] monsterPrefabs;

    // 변수
    private int monsterCount = 0;
    public int MonsterCount
    {
        get { return monsterCount; }
        set
        {
            if (value <= 0)
            {
                room.EndEvent();
                monsterCount = 0;
            }
            else monsterCount = value;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DestroyMonster();
        }
    }

    // 몬스터 스포너 초기화
    public void Init(RoomHandler _room)
    {
        room = _room;
    }

    // 몬스터 소환
    public void SpawnMosnters()
    {
        monsterCount = Random.Range(4, 9);

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

    // 테스트용 : 몬스터 삭제
    private void DestroyMonster()
    {
        if (MonsterCount == 0) return;

        MonsterCount--;
        monsters[MonsterCount].gameObject.SetActive(false);
    }
}
