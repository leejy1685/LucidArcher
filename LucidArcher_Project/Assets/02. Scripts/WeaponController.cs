using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] SpriteRenderer weaponRenderer;
    public void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
