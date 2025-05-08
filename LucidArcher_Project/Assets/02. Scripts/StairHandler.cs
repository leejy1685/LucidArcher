using System.Collections;
using UnityEngine;

public class StairHandler : MonoBehaviour
{
    // 상수

    // 외부 오브젝트
    [SerializeField] private GameObject frontTile;

    // 프리팹

    // 변수
    private bool canGoDown = false;

    // gameObject 컴포넌트

    public void MoveFrontTile()
    {
        frontTile.SetActive(false);
        canGoDown = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canGoDown && collision.CompareTag("Player"))
        {
            canGoDown = false;
            RoomSpawner.Instance.MoveNextFloor(collision.transform.position);
        }
    }
}
