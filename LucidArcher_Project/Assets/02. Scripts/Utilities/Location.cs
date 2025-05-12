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
}
