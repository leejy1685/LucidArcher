using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 외부 오브젝트
    [SerializeField] private Transform initialTarget;
    private float initialCameraSize;
    private Transform target;

    // 변수
    private float cameraHalfWidth;
    private float cameraHalfHeight;
    private Vector3 roomPosition;
    private float maxX;
    private float maxY;
    private float smoothDegree;

    private void Awake()
    {
        initialCameraSize = Camera.main.orthographicSize;
        UpdateCameraSize(initialCameraSize);
        SetOriginTarget();
        maxX = cameraHalfWidth;
        maxY = cameraHalfHeight;
    }

    private void Update()
    {
        SmoothMoveToTarget(target.position);
    }

    // 카메라 타켓 초기화
    public void SetOriginTarget()
    {
        target = initialTarget;
        smoothDegree = 0.01f;
    }

    // 카메라 사이즈 업데이트
    private void UpdateCameraSize(float cameraSize)
    {
        cameraHalfHeight = cameraSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;
    }

    // 부드러운 카메라 이동
    private void SmoothMoveToTarget(Vector3 _targetPosition)
    {
        float cameraX = CheckPositionLimit(_targetPosition.x, true);
        float cameraY = CheckPositionLimit(_targetPosition.y, false);

        Vector3 limitedPosition = new Vector3(cameraX, cameraY, -10);

        transform.position = Vector3.Lerp(transform.position, limitedPosition, smoothDegree);
        if ((transform.position - limitedPosition).magnitude < 0.01f)
        {
            transform.position = limitedPosition;
        }
    }

    // X or Y 값이 제한 범위를 나갔는지 체크
    private float CheckPositionLimit(float value, bool isX)
    {
        float limitedValue = value;
        float center = isX ? roomPosition.x : roomPosition.y;
        float max = isX ? maxX - cameraHalfWidth : maxY - cameraHalfHeight;

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
    public void UpdateRoomLimit(Vector3 _roomPosition, float _maxX, float _maxY)
    {
        roomPosition = _roomPosition;
        maxX = _maxX;
        maxY = _maxY;
    }

    // 카메라 타겟 변경
    public void SetTarget(Transform _target, float _smoothDegree = 0.01f)
    {
        target = _target;
        smoothDegree = _smoothDegree;
    }

    // 타겟 Zoom-IN
    public IEnumerator ZoomInTarget(float zoomSize, float duration)
    {
        yield return Zoom(initialCameraSize, zoomSize, duration);
    }

    // 타겟 Zoom-Out
    public IEnumerator ZoomOutTarget(float zoomSize, float duration)
    {
        yield return Zoom(zoomSize, initialCameraSize, duration);
    }

    // 줌 효과 시 사용되는 코루틴
    IEnumerator Zoom(float startSize, float endSize, float duration)
    {
        float timer = 0f;
        UpdateCameraSize(endSize);

        while (timer < duration)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startSize, endSize, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        Camera.main.orthographicSize = endSize;
    }
}
