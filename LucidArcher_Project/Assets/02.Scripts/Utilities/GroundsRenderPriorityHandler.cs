using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundsRenderPriorityHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    private void Update()
    {
        sprite.sortingOrder = (int)(sprite.transform.position.y * -100);
    }
}
