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
    private GameObject player;

    // 프리팹
    [Header("Prefabs")]
    [SerializeField] private GameObject stairPrefab;
    [SerializeField] private GameObject Chest;

    // 변수
    private RoomState roomState;
    private bool isExcuted = false;

    private CameraController cameraController;

    private void Awake()
    {
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();
    }

    // 방의 위치와 상태 등 초기화
    public void InitRoom(RoomState roomState, Vector3 position, GameObject _player)
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
        isExcuted = true;
        exitDetector.SetActive(true);

        cameraController.SetTarget(gate.NearestGate(player.transform.position));
        yield return cameraController.ZoomInTarget(ZOOM_SIZE, ZOOM_DURATION);

        gate.ControllGate(true);
        yield return WAIT_ONE_SEC;

        cameraController.SetOriginTarget();
        yield return cameraController.ZoomOutTarget(ZOOM_SIZE, ZOOM_DURATION);

        // 이벤트 실행
        monsterSpawner.SpawnMosnters();
    }

    // 이벤트 종료 후 경험치, 아이템 등 획득 / 보스 방이면 계단도 보이게
    public IEnumerator EndEvent()
    {
        Transform target = roomState == RoomState.Boss ? stair.transform : gate.NearestGate(player.transform.position);

        cameraController.SetTarget(target);
        yield return cameraController.ZoomInTarget(ZOOM_SIZE, ZOOM_DURATION);

        if (roomState == RoomState.Boss)
            stair.GetComponent<StairHandler>().MoveFrontTile();
        else
            gate.ControllGate(false);
        yield return WAIT_ONE_SEC;

        cameraController.SetOriginTarget();
        yield return cameraController.ZoomOutTarget(ZOOM_SIZE, ZOOM_DURATION);

        // 경험치, 아이템 등 획득
        if (Random.Range(1, 101) > 50) SpawnChest();
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
    private void SpawnChest()
    {
        GameObject chest = Instantiate(Chest, transform.position, Quaternion.identity, transform);
    }
}
