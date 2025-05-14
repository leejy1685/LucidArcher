using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 외부 오브젝트
    private RoomHandler room;

    // 프리팹
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private SpawnAnimation spawnAnimationPrefab;

    // 변수
    private int monsterCount;
    public int MonsterCount { get { return monsterCount; } }

    private int actualSpawnCount = 1; // 테스트용

    private void Update()
    {
        DestroyAllMonster();
    }

    // 몬스터 스포너 초기화
    public void Init(RoomHandler _room)
    {
        room = _room;
    }

    // 몬스터 랜덤 소환
    public IEnumerator SpawnAllMonsters()
    {
        monsterCount = Random.Range(4, 9);
        actualSpawnCount = monsterCount; // 테스트 코드

        for (int i = 0; i < monsterCount; i++)
        {
            StartCoroutine(SpawnMonster());
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SpawnMonster()
    {
        Vector3 spawnPosition = room.RandomPosition();

        SpawnAnimation spawnAnimation = Instantiate(spawnAnimationPrefab, spawnPosition, Quaternion.identity, transform);
        yield return spawnAnimation.DrawSpawnCircle();

        GameObject monster = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], transform);
        monster.GetComponent<MonsterBase>().Init(this, spawnPosition);

        actualSpawnCount--; // 테스트 코드
    }

    // 몬스터 죽었을 때, 몬스터 수 감소 업데이트
    public void DecreaseMonsterCount()
    {
        monsterCount--;

        if(monsterCount <= 0)
        {
            StartCoroutine(room.EndEvent());
        }
    }

    // 테스트용 : 몬스터 삭제
    private void DestroyAllMonster()
    {
        if (actualSpawnCount == 0 && Input.GetKeyDown(KeyCode.Z))
        {
            MonsterBase[] monsters = GetComponentsInChildren<MonsterBase>();
            foreach (MonsterBase monster in monsters)
            {
                monster.gameObject.SetActive(false);
                DecreaseMonsterCount();
            }
        }
    }
}
