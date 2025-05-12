using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public float rotateDegreePerSecond;

    [SerializeField] private float startAngle;
    [SerializeField] private float elapsedTime;

    private void Start()
    {
        startAngle = transform.eulerAngles.z;
    }

    private void OnEnable()
    {
        startAngle = transform.eulerAngles.z;
        elapsedTime = 0;
    }

    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, startAngle + elapsedTime * rotateDegreePerSecond);
    }
}
