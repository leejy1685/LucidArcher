using UnityEngine;

public class ExitDetection : MonoBehaviour
{
    private bool isSpawn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSpawn && collision.CompareTag("Player"))
        {
            RoomSpawner.Instance.SpawnRoom(transform.position + transform.localPosition);
            isSpawn = true;
        }
    }
}
