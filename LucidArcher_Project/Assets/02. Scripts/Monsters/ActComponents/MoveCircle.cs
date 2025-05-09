using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    [SerializeField] private float radius = 2.4f;

    float time = 0;


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        float posX = Mathf.Sin(time) * radius;
        float posY = Mathf.Cos(time) * radius;
        transform.position = new Vector3(posX , posY, transform.position.z);
    }
}
