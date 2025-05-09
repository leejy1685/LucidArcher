using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    // 상수
    private const int X_MAX = 12;
    private const int Y_MAX = 6;

    // 외부 오브젝트
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject exitDetection;
    private GameObject stair;

    // 프리팹
    [SerializeField] private GameObject stairPrefab;

    // 변수
    private RoomState roomState;
    private bool isExcuted = false;

    private void Update()
    {
        // 임시 테스트용
        if(Input.GetKeyDown(KeyCode.Space))
            EndEvent();
    }

    // 방의 위치와 상태 초기화
    public void InitRoom(RoomState roomState, Vector3 position)
    {
        this.roomState = roomState;
        transform.position = position;

        if(roomState == RoomState.Start)
        {
            exitDetection.SetActive(true);
        }
        else
        {
            exitDetection.SetActive(false);
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
    }

    // 방에 진입했을 때 이벤트 실행 (적 소환 등)
    private void ExcuteEvent()
    {
        isExcuted = true;
        
        // 적 소환 등 실행

        ControllGate(false);
    }

    // 이벤트 종료 후 경험치, 아이템 등 획득 / 보스 방이면 계단도 보이게
    private void EndEvent()
    {
        // 경험치, 아이템 등 획득

        if (roomState != RoomState.Boss)
        {
            exitDetection.SetActive(true);
            ControllGate(true);
        }
        else
        {
            stair.GetComponent<StairHandler>().MoveFrontTile();
        }
    }

    // 방 파괴
    public void DestroyRoom()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isExcuted && collision.CompareTag("Player"))
        {
            if(roomState == RoomState.Start) return;

            ExcuteEvent();
        }
    }
}
