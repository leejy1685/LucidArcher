using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundsRenderPriorityHandler : MonoBehaviour
{
    // ��������Ʈ sorting layer�� GroundObjects���� Ȯ�����ּ���
    [SerializeField] SpriteRenderer sprite;

    private void Update()
    {
        sprite.sortingOrder = (int)(sprite.transform.position.y * -100);
    }
}
