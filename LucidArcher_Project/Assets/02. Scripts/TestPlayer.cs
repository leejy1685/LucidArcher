using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 addPos = new Vector3(x, y, 0);

        transform.position += addPos * 15f * Time.deltaTime;
    }
}
