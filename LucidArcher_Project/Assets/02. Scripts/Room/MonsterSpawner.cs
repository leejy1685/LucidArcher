using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 외부 오브젝트
    private RoomHandler room;
    private List<MonsterBase> monsters = new List<MonsterBase>();

    // 프리팹
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private SpawnAnimation spawnAnimationPrefab;

    // 변수
    private bool isSpawn = false;
    private int monsterCount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isSpawn)
        {
            DestroyAllMonster();
        }
    }

    // 몬스터 스포너 초기화
    public void Init(RoomHandler _room)
    {
        room = _room;
    }

    // 몬스터 랜덤 소환
    public IEnumerator SpawnAllMonsters()
    {
        isSpawn = true;

        for (int i = 0; i < Random.Range(4, 9); i++)
        {
            StartCoroutine(SpawnMonster());
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SpawnMonster()
    {
        Vector3 spawnPosition = RandomPosition();

        SpawnAnimation spawnAnimation = Instantiate(spawnAnimationPrefab, spawnPosition, Quaternion.identity, transform);
        yield return spawnAnimation.DrawSpawnCircle();

        GameObject monster = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], transform);
        monster.GetComponent<MonsterBase>().Init(this, spawnPosition);
        monsters.Add(monster.GetComponent<MonsterBase>());

        UpdateMonsterCount();
    }

    // 랜덤 포지션 반환
    private Vector3 RandomPosition()
    {
        Vector2 colliderSize = room.GetComponent<BoxCollider2D>().size;

        float x = Random.Range(-colliderSize.x * 0.5f, colliderSize.x * 0.5f);
        float y = Random.Range(-colliderSize.y * 0.5f, colliderSize.y * 0.5f);

        return transform.position + new Vector3(x, y, 0);
    }

    // 몬스터 수 업데이트
    private void UpdateMonsterCount()
    {
        monsterCount = monsters.Count;

        if(monsterCount == 0)
        {
            isSpawn = false;
            StartCoroutine(room.EndEvent());
        }
    }

    // 몬스터 죽었을 때, 몬스터 제거
    public void DestroyMonster(MonsterBase monster)
    {
        monsters.Remove(monster);
        monster.gameObject.SetActive(false);
        
        UpdateMonsterCount();
    }

    // 테스트용 : 몬스터 삭제
    private void DestroyAllMonster()
    {
        foreach(MonsterBase monster in monsters)
        {
            monster.gameObject.SetActive(false);
        }
        monsters.Clear();

        UpdateMonsterCount();
    }
}
