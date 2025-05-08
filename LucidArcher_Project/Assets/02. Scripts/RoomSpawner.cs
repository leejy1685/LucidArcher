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
    [SerializeField] RoomController startRoom;
    [SerializeField] RoomController bossRoom;
    [SerializeField] RoomController[] rooms;

    // 변수
    private RoomController previousRoom;
    private RoomController currentRoom;
    private int roomCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        roomCount = 0;
        SpawnRoom(Vector3.zero);
    }

    public void SpawnRoom(Vector3 detectionPosition)
    {
        if (roomCount > MAX_ROOM) return;

        if(currentRoom != null) previousRoom = currentRoom;

        switch (roomCount)
        {
            case 0:
                currentRoom = Instantiate(startRoom, transform);
                currentRoom.InitRoom(RoomState.Start, Vector3.zero);
                break;

            case MAX_ROOM:
                currentRoom = Instantiate(bossRoom, transform);
                currentRoom.InitRoom(RoomState.Boss, detectionPosition);
                break;

            default:
                currentRoom = Instantiate(rooms[Random.Range(0, rooms.Length)], transform);
                currentRoom.InitRoom(RoomState.Enemy, detectionPosition);
                break;
        }

        previousRoom?.gameObject.SetActive(false);
        roomCount++;
    }
}
