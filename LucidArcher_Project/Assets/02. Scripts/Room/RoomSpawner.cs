using System.Collections;
using UnityEngine;

public enum RoomState
{
    Start,
    Enemy,
    Boss,
}

public class RoomSpawner : MonoBehaviour
{
    // 싱글톤
    public static RoomSpawner Instance { get; private set; }

    // 상수
    private const int MAX_ROOM = 6;

    // 외부 오브젝트
    [SerializeField] private GameObject player;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private UIManager uiManager;

    // 프리팹
    [SerializeField] RoomHandler startRoom;
    [SerializeField] RoomHandler bossRoom;
    [SerializeField] RoomHandler[] rooms;

    // 변수
    private RoomHandler previousRoom;
    private RoomHandler currentRoom;
    private int roomCount;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        roomCount = 0;
        SpawnRoom(Vector3.zero);
    }

    // 방 소환
    public void SpawnRoom(Vector3 initPosition)
    {
        if (roomCount >= MAX_ROOM) return;

        previousRoom?.gameObject.SetActive(false);
        if (currentRoom != null) previousRoom = currentRoom;
        roomCount++;

        switch (roomCount)
        {
            case 1:
                currentRoom = Instantiate(startRoom, transform);
                currentRoom.InitRoom(RoomState.Start, initPosition, player);
                break;

            case MAX_ROOM:
                currentRoom = Instantiate(bossRoom, transform);
                currentRoom.InitRoom(RoomState.Boss, initPosition, player);
                break;

            default:
                currentRoom = Instantiate(rooms[Random.Range(0, rooms.Length)], transform);
                currentRoom.InitRoom(RoomState.Enemy, initPosition, player);
                break;
        }
    }

    // 다음 층으로 이동
    public IEnumerator MoveNextFloor(Transform initTransform)
    {
        Vector3 initPosition = initTransform.position;
        
        cameraController.SetTarget(initTransform);
        uiManager.FadeOut();
        yield return cameraController.ZoomInTarget(1f, 1f);

        cameraController.SetOriginTarget();
        DestroyAllRoom();
        roomCount = 0;
        SpawnRoom(initPosition);

        uiManager.FadeIn();
        StartCoroutine(cameraController.ZoomOutTarget(1f, 1f));
    }

    // 모든 방 파괴
    private void DestroyAllRoom()
    {
        previousRoom = null;
        currentRoom = null;

        RoomHandler[] objs = GetComponentsInChildren<RoomHandler>(true);

        foreach (RoomHandler obj in objs)
        {
            obj.DestroyRoom();
        }
    }
}
