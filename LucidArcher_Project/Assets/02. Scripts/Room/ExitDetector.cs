using UnityEngine;

public class ExitDetector : MonoBehaviour
{
    private bool isSpawn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSpawn && collision.CompareTag("Player"))
        {
            GameManager.Instance.CreateRoom(transform.position + transform.localPosition);
            isSpawn = true;
        }
    }
}
