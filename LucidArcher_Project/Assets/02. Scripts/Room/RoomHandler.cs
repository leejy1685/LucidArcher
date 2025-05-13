using System.Collections;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    // 상수
    private const int X_MAX = 12;
    private const int Y_MAX = 6;
    private static readonly int IS_GATE_UP = Animator.StringToHash("IsUp");
    private static readonly WaitForSeconds WAIT_HALF_SEC = new WaitForSeconds(0.5f);
    private static readonly WaitForSeconds WAIT_ONE_SEC = new WaitForSeconds(1f);

    // 외부 오브젝트
    [Header("GameObjects")]
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject exitDetector;
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private Animator gateAnimator;
    private GameObject stair;

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

    // 방의 위치와 상태 초기화
    public void InitRoom(RoomState roomState, Vector3 position)
    {
        this.roomState = roomState;
        transform.position = position;
        float maxX = exitDetector.transform.GetChild(0).localPosition.x;
        float maxY = exitDetector.transform.GetChild(2).localPosition.y;
        cameraController.UpdateCameraLimit(position, maxX, maxY);
        monsterSpawner.Init(this);

        if (roomState == RoomState.Start)
        {
            exitDetector.SetActive(true);
        }
        else
        {
            exitDetector.SetActive(false);
        }

        if(roomState == RoomState.Boss)
        {
            int randX = Random.Range(-X_MAX, X_MAX);
            int randY = Random.Range(-Y_MAX, Y_MAX);
            stair = Instantiate(stairPrefab, transform);
            stair.transform.localPosition = new Vector3(randX + 0.5f, randY + 0.5f);

            stair.SetActive(true);
        }

        ControllGate(true);
    }

    // 게이트 오브젝트 활성화/비활성화
    private void ControllGate(bool isOpen)
    {
        gate.SetActive(!isOpen);
        gateAnimator.SetBool(IS_GATE_UP, !isOpen);
    }

    // 방에 진입했을 때 이벤트 실행 (적 소환 등)
    private void ExcuteEvent()
    {
        isExcuted = true;
        exitDetector.SetActive(true);

        StartCoroutine(CoroutineExcuteEvent());
    }
    IEnumerator CoroutineExcuteEvent()
    {
        cameraController.ChangeTarget(gateAnimator.transform.parent);
        yield return WAIT_ONE_SEC;

        ControllGate(false);

        //문 닫는 소리
        SoundManager.PlayClip(closeSound);
        yield return WAIT_ONE_SEC;

        cameraController.ChangeTarget();
        //전투 브금
        SoundManager.instance.StartBattle();
        yield return WAIT_HALF_SEC;

        // 이벤트 실행
        monsterSpawner.SpawnMosnters();
    }


    // 이벤트 종료 후 경험치, 아이템 등 획득 / 보스 방이면 계단도 보이게
    public void EndEvent()
    {
        if (roomState == RoomState.Start) return;

        StartCoroutine(CoroutineEndEvent());
    }
    IEnumerator CoroutineEndEvent()
    {
        if(roomState == RoomState.Boss)
        {
            cameraController.ChangeTarget(stair.transform);
        }
        else
        {
            cameraController.ChangeTarget(gateAnimator.transform.parent);
        }
        //전투 종료 브금
        SoundManager.instance.EndBattle();
        yield return WAIT_ONE_SEC;

        if (roomState == RoomState.Boss)
        {
            stair.GetComponent<StairHandler>().MoveFrontTile();
        }
        else
        {
            ControllGate(true);
        }
        //문 여는 소리
        SoundManager.PlayClip(openSound);

        yield return WAIT_ONE_SEC;

        cameraController.ChangeTarget();
        yield return WAIT_HALF_SEC;

        // 경험치, 아이템 등 획득
        SpawnChest();
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

            ExcuteEvent();
        }
    }

    // 상자 소환
    private void SpawnChest()
    {
        GameObject chest = Instantiate(Chest, transform.position, Quaternion.identity, transform);
    }
}
