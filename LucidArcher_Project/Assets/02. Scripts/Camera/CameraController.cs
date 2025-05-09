using TMPro;
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
        SmoothMoveToTarget(target.position);
    }

    // 부드러운 카메라 이동
    private void SmoothMoveToTarget(Vector3 targetPosition)
    {
        float cameraX = CheckPositionLimit(targetPosition.x, true);
        float cameraY = CheckPositionLimit(targetPosition.y, false);

        Vector3 limitedPosition = new Vector3(cameraX, cameraY, -10);

        transform.position = Vector3.Lerp(transform.position, limitedPosition, 0.01f);
        if((transform.position - limitedPosition).magnitude < 0.01f)
        {
            transform.position = limitedPosition;
        }
    }

    // X or Y 값이 제한 범위를 나갔는지 체크
    private float CheckPositionLimit(float value, bool isX)
    {
        float limitedValue = value;
        float center = isX ? roomPosition.x : roomPosition.y;
        float max = isX ? maxX : maxY;

        if (limitedValue > center + max)
        {
            limitedValue = center + max;
        }
        else if (limitedValue < center - max)
        {
            limitedValue = center - max;
        }

        return limitedValue;
    }

    // 카메라 제한 범위 업데이트
    public void UpdateCameraLimit(Vector3 _roomPosition, float _maxX, float _maxY)
    {
        roomPosition = _roomPosition;
        maxX = _maxX - cameraHalfWidth;
        maxY = _maxY - cameraHalfHeight;
    }

    // 기존 카메라 이동 방식(너무 경직되있음) : 현재 사용 안함
    private void MoveToTarget(Vector3 targetPosition)
    {
        float cameraX = CheckPositionLimit(targetPosition.x, true);
        float cameraY = CheckPositionLimit(targetPosition.y, false);

        transform.position = new Vector3(cameraX, cameraY, -10);
    }
}
