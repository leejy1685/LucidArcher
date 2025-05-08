using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] MonsterBase detector;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����ȭ �ʿ�� ���̾� �񱳷�
        if (collision.CompareTag("Player"))
        {
            detector.OnPlayerDetected(collision.gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // ����ȭ �ʿ�� ���̾� �񱳷�
        if (collision.CompareTag("Player"))
        {
            detector.OnPlayerMissed();
        }

    }
}
