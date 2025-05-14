using System.Collections;
using UnityEngine;

public class ExitDetector : MonoBehaviour
{
    // 외부 오브젝트
    [SerializeField] private GameObject colliderBox;

    // 변수
    private bool isSpawn = false;

    private void Start()
    {
        colliderBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSpawn && collision.CompareTag("Player"))
        {
            RoomSpawner.Instance.SpawnRoom(transform.position + transform.localPosition);
            colliderBox.SetActive(true);
            StartCoroutine(MoveCollider());
            isSpawn = true;
        }
    }

    IEnumerator MoveCollider()
    {
        Vector3 origin = colliderBox.transform.position;
        Vector3 target = transform.position;

        float timer = 0f;
        while(timer <= 1f)
        {
            colliderBox.transform.position = Vector3.Lerp(origin, target, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        colliderBox.transform.position = target;
    }
}
