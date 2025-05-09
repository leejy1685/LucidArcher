using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ShootBullet(Vector2 lookDirection,float speed)
    {
        Vector2 velocity = lookDirection.normalized * speed;
        rigidbody2D.velocity = velocity;
    }
}
