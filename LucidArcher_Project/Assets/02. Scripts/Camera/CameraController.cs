using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 외부 오브젝트
    [SerializeField] private Transform initialTarget;
    private Transform target;

    // 변수
    private float cameraHalfWidth;
    private float cameraHalfHeight;
    private Vector3 roomPosition;
    private float maxX;
    private float maxY;

    private bool hasLimit = true;

    private void Awake()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

        target = initialTarget;
    }

    private void Update()
    {
        SmoothMoveToTarget(target.position, hasLimit);
    }

    // 부드러운 카메라 이동
    private void SmoothMoveToTarget(Vector3 _targetPosition, bool hasLimit)
    {
        Vector3 targetPosition;
        if (hasLimit)
        {
            float cameraX = CheckPositionLimit(_targetPosition.x, true);
            float cameraY = CheckPositionLimit(_targetPosition.y, false);

            targetPosition = new Vector3(cameraX, cameraY, -10);
        }
        else
        {
            targetPosition = new Vector3(_targetPosition.x, _targetPosition.y, -10);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.01f);
        if ((transform.position - targetPosition).magnitude < 0.01f)
        {
            transform.position = targetPosition;
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

    // 카메라 타겟 변경
    public void ChangeTarget(Transform _target, float orthographicSize, float duration)
    {
        target = _target;
        ZoomIn(orthographicSize, duration);
        hasLimit = false;
    }
    public void ChangeTarget(Transform _target, float orthographicSize)
    {
        target = _target;
        ZoomIn(orthographicSize, 1f);
        hasLimit = false;
    }
    public void ChangeTarget()
    {
        target = initialTarget;
        ZoomOut(3f, 1f);
        hasLimit = true;
    }

    // 줌인 효과
    private void ZoomIn(float size, float duration)
    {
        StartCoroutine(Zoom(cameraHalfHeight, size, duration));
    }

    // 줌아웃 효과
    private void ZoomOut(float size, float duration)
    {
        StartCoroutine(Zoom(size, cameraHalfHeight, duration));
    }

    // 줌 효과 시 사용되는 코루틴
    IEnumerator Zoom(float startSize, float endSize, float duration)
    {
        float timer = 0f;

        while(timer < duration)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startSize, endSize, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        Camera.main.orthographicSize = endSize;
    }
}
