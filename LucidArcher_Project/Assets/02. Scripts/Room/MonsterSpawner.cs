using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 외부 오브젝트
    private RoomHandler room;

    // 프리팹
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private GameObject bossPrefab;
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

    // 랜덤 몬스터 여러 마리 소환
    public IEnumerator SpawnAllMonsters(RoomState roomState)
    {
        monsterCount = roomState == RoomState.Boss ? 1 : Random.Range(4, 9);
        actualSpawnCount = monsterCount; // 테스트 코드

        for (int i = 0; i < monsterCount; i++)
        {
            StartCoroutine(SpawnMonster(roomState));
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 랜덤 몬스터 한마리 소환
    private IEnumerator SpawnMonster(RoomState roomState)
    {
        Vector3 spawnPosition = roomState == RoomState.Boss ? transform.position : room.RandomPosition();

        SpawnAnimation spawnAnimation = Instantiate(spawnAnimationPrefab, spawnPosition, Quaternion.identity, transform);
        yield return spawnAnimation.DrawSpawnCircle();

        GameObject monster = Instantiate(
            roomState == RoomState.Boss ? bossPrefab : monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], transform);
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
