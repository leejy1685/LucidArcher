using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 외부 오브젝트
    [SerializeField] private Transform target;

    // 변수
    private float cameraHalfWidth;
    private float cameraHalfHeight;
    private Vector3 roomPosition;
    private float maxX;
    private float maxY;

    private void Start()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;
    }

    private void Update()
    {
        transform.position = UpdateCameraPosition(target.position.x, target.position.y);
    }

    private Vector3 UpdateCameraPosition(float x, float y)
    {
        float cameraX = x;
        float cameraY = y;

        if(x > roomPosition.x + maxX)
        {
            cameraX = roomPosition.x + maxX;
        }
        else if(x < roomPosition.x - maxX)
        {
            cameraX = roomPosition.x - maxX;
        }

        if (y > roomPosition.y + maxY)
        {
            cameraY = roomPosition.y + maxY;
        }
        else if (y < roomPosition.y - maxY)
        {
            cameraY = roomPosition.y - maxY;
        }

        return new Vector3(cameraX, cameraY, -10);
    }

    public void UpdateMovablePosition(Vector3 _roomPosition, float _maxX, float _maxY)
    {
        roomPosition = _roomPosition;
        maxX = _maxX - cameraHalfWidth;
        maxY = _maxY - cameraHalfHeight;
    }
}
