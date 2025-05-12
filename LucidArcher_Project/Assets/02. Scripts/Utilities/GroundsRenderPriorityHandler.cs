using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundsRenderPriorityHandler : MonoBehaviour
{
    // 스프라이트 sorting layer가 GroundObjects인지 확인해주세요
    [SerializeField] SpriteRenderer sprite;

    private void Update()
    {
        sprite.sortingOrder = (int)(sprite.transform.position.y * -100);
    }
}
