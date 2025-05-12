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
                currentRoom.InitRoom(RoomState.Start, initPosition);
                break;

            case MAX_ROOM:
                currentRoom = Instantiate(bossRoom, transform);
                currentRoom.InitRoom(RoomState.Boss, initPosition);
                break;

            default:
                currentRoom = Instantiate(rooms[Random.Range(0, rooms.Length)], transform);
                currentRoom.InitRoom(RoomState.Enemy, initPosition);
                break;
        }
    }

    // 다음 층으로 이동
    public void MoveNextFloor(Vector3 initPosition)
    {
        DestroyAllRoom();

        roomCount = 0;
        SpawnRoom(initPosition);
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
