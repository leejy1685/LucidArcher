using System.Collections;
using UnityEngine;

public class SpawnAnimation : MonoBehaviour
{
    // 변수
    [SerializeField] private float radius = 1f;
    [SerializeField] private float duration = 1f;
    private int segments = 100;
    private float timer;

    // 컴포넌트
    LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public IEnumerator DrawSpawnCircle()
    {
        while (true)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            int pointsToDraw = Mathf.FloorToInt(segments * t);

            line.positionCount = pointsToDraw;

            for (int i = 0; i < pointsToDraw; i++)
            {
                float angle = (float)i / segments * Mathf.PI * 2f;
                Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                line.SetPosition(i, pos);
            }

            if (t >= 1f)
            {
                gameObject.SetActive(false);
                break;
            }

            yield return null;
        }
    }
}
