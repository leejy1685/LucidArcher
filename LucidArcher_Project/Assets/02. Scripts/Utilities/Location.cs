using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public Vector2 size = new Vector2(5f, 5f);
    public Color gizmoColor = new Color(1f, 0f, 0f, 0.3f); // 반투명 빨간색

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
    public Rect GetRect()
    {
        Vector2 center = (Vector2)transform.position;
        Vector2 bottomLeft = center - size * 0.5f;
        return new Rect(bottomLeft, size);
    }
    public Vector2 GetRandomPointInRect()   // 필드 랜덤 패턴 사용 목적
    {
        Rect rect = GetRect();
        float x = Random.Range(rect.xMin, rect.xMax);
        float y = Random.Range(rect.yMin, rect.yMax);
        return new Vector2(x, y);
    }
}
