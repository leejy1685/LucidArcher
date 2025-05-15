using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamWave : MonoBehaviour
{
    public RawImage ri;
    Material material;
    float power = 0.05f;
    float duration = 3;
    float elapsedTime = 0;
    private void Awake()
    {
        material = ri.material;
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
        power = 0.05f - 0.05f * elapsedTime / duration; 
        material.SetFloat("_Power", power);

        if(elapsedTime > duration) Destroy(gameObject);

    }
}
