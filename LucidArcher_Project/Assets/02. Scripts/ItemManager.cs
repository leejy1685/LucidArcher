using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{

    //private string itemName; //������ �̸��ε� �� �� ���� �� ������ ���� �ʿ��� �� �� ����?



    public float pickupDelay = 0.5f; // ������ �ٷ� �Ծ����� �ȵǴ� ������
    public float spawnTime; // ������ ������ �ð� ����


    private void Start()
    {
        spawnTime = Time.time;  //������ ������ �ð� ���
    }

    public abstract void ItemAction(GameObject player);  //�ڽ� Ŭ�������� ���� �� �޼���


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (Time.time - spawnTime < pickupDelay)  // �����ǰ� �ε��� �ð��� pickupDelay���� ������ �ȸԾ���
            { 
                return;

            }
            ItemAction(other.gameObject); //������ ���� ȿ�� �ߵ�

        }
    }
}
