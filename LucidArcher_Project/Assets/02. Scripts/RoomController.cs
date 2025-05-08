using UnityEngine;

public class RoomController : MonoBehaviour
{
    // 외부 오브젝트
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject exitDetection;

    // 변수
    private RoomState roomState;

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
        exitDetection.SetActive(false);

        ControllGate(true);
    }

    // 게이트 오브젝트 활성화/비활성화
    private void ControllGate(bool isOpen)
    {
        gate.SetActive(!isOpen);
    }

    // 방에 진입했을 때 이벤트 실행 (몬스터 소환, 보스 소환 등)
    private void ExcuteEvent()
    {
        ControllGate(false);
    }

    // 이벤트 종료 후 경험치, 아이템 등 획득 / 보스 방이면 계단도 보이게
    private void EndEvent()
    {
        ControllGate(true);
        exitDetection.SetActive(true);
    }

    // 룸 파괴
    public void DestroyRoom()
    {
        Destroy(gameObject);
    }
}
