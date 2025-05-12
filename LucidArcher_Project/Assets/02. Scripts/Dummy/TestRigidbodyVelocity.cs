using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRigidbodyVelocity : MonoBehaviour
{
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.right;
    }
}
