using System.Collections;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    // 상수
    private const int X_MAX = 12;
    private const int Y_MAX = 6;
    private const float ZOOM_SIZE = 3f;
    private const float ZOOM_DURATION = 0.5f;
    private static readonly WaitForSeconds WAIT_ONE_SEC = new WaitForSeconds(1f);

    // 외부 오브젝트
    [Header("GameObjects")]
    [SerializeField] private GateHandler gate;
    [SerializeField] private GameObject exitDetector;
    [SerializeField] private MonsterSpawner monsterSpawner;
    private GameObject stair;
    private Transform player;

    // 프리팹
    [Header("Prefabs")]
    [SerializeField] private GameObject stairPrefab;
    [SerializeField] private GameObject Chest;

    // 변수
    private RoomState roomState;
    private bool isExcuted = false;

    private CameraController cameraController;

    //게임 매니저에서 사용
    public MonsterSpawner MonsterSpawner { get { return monsterSpawner; } }

    //소리 추가
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    private void Awake()
    {
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();
    }

    // 방의 위치와 상태 등 초기화
    public void InitRoom(RoomState roomState, Vector3 position, Transform _player)
    {
        this.roomState = roomState;
        transform.position = position;
        monsterSpawner.Init(this);
        player = _player; // 일단 테스트 용

        cameraController.UpdateRoomLimit(position,
            exitDetector.transform.GetChild(3).localPosition.x, exitDetector.transform.GetChild(0).localPosition.y);

        exitDetector.SetActive(roomState == RoomState.Start);

        if(roomState == RoomState.Boss)
        {
            int randX = Random.Range(-X_MAX, X_MAX);
            int randY = Random.Range(-Y_MAX, Y_MAX);
            stair = Instantiate(stairPrefab, transform);
            stair.transform.localPosition = new Vector3(randX + 0.5f, randY + 0.5f);

            stair.SetActive(true);
        }
    }

    // 방에 진입했을 때 이벤트 실행 (적 소환 등)
    private IEnumerator ExcuteEvent()
    {
        GameManager.Instance.PauseGame(true);

        isExcuted = true;
        exitDetector.SetActive(true);

        cameraController.SetTarget(gate.NearestGate(player.position));
        yield return cameraController.ZoomInTarget(ZOOM_SIZE, ZOOM_DURATION);

        gate.ControllGate(true);
        yield return WAIT_ONE_SEC;

        SoundManager.PlayClip(closeSound);

        cameraController.SetOriginTarget();
        yield return cameraController.ZoomOutTarget(ZOOM_SIZE, ZOOM_DURATION);

        //전투 브금
        SoundManager.instance.StartBattle();

        // 이벤트 실행
        StartCoroutine(monsterSpawner.SpawnAllMonsters(roomState));

        GameManager.Instance.PauseGame(false);
    }

    // 이벤트 종료 후 경험치, 아이템 등 획득 / 보스 방이면 계단도 보이게
    public IEnumerator EndEvent()
    {
        GameManager.Instance.PauseGame(true);

        SoundManager.instance.EndBattle();

        Transform target = roomState == RoomState.Boss ? stair.transform : gate.NearestGate(player.position);

        cameraController.SetTarget(target);
        yield return cameraController.ZoomInTarget(ZOOM_SIZE, ZOOM_DURATION);

        if (roomState == RoomState.Boss)
            stair.GetComponent<StairHandler>().MoveFrontTile();
        else
            gate.ControllGate(false);

        SoundManager.PlayClip(openSound);

        yield return WAIT_ONE_SEC;

        // 경험치, 아이템 등 획득 : 현재 30%
        if (Random.Range(1, 101) <= 30)
        {
            yield return SpawnChest();
        }

        cameraController.SetOriginTarget();
        yield return cameraController.ZoomOutTarget(ZOOM_SIZE, ZOOM_DURATION);

        GameManager.Instance.PauseGame(false);
    }

    // 방 파괴
    public void DestroyRoom()
    {
        Destroy(gameObject);
    }

    // 플레이어가 어느 정도 방 안에 들어오면 이벤트 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isExcuted && collision.CompareTag("Player"))
        {
            if(roomState == RoomState.Start) return;

            StartCoroutine(ExcuteEvent());
        }
    }

    // 상자 소환
    private IEnumerator SpawnChest()
    {
        GameObject chest = Instantiate(Chest, transform);
        Vector3 spawnPosition = RandomPosition();
        
        chest.transform.position = spawnPosition + new Vector3(0, 3, 0);
        chest.GetComponent<Collider2D>().enabled = false;

        cameraController.SetTarget(chest.transform);

        float speed = 0f;
        while (chest.transform.position != spawnPosition)
        {
            speed = speed + 9.8f * Time.deltaTime;
            chest.transform.position = Vector3.MoveTowards(chest.transform.position, spawnPosition, speed * Time.deltaTime);
            yield return null;
        }

        chest.GetComponent<Collider2D>().enabled = true;
    }

    // 소환가능한 랜덤 위치 반환
    public Vector3 RandomPosition()
    {
        Vector2 colliderSize = GetComponent<BoxCollider2D>().size;

        float x = Random.Range(-colliderSize.x * 0.5f, colliderSize.x * 0.5f);
        float y = Random.Range(-colliderSize.y * 0.5f, colliderSize.y * 0.5f);

        return transform.position + new Vector3(x, y, 0);
    }
}
