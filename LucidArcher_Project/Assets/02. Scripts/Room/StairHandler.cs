using UnityEngine;

public class StairHandler : MonoBehaviour
{
    // 외부 오브젝트
    [SerializeField] private GameObject frontTile;

    // 변수
    private bool canGoDown = false;

    public void MoveFrontTile()
    {
        frontTile.GetComponent<Animator>().SetTrigger("Move");
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
