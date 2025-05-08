using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{

    //private string itemName; //������ �̸��ε� �� �� ���� �� ������ ���� �ʿ��� �� �� ����?


    // Update is called once per frame


    //������ ȿ�� �ߵ�
    public abstract void ItemAction(GameObject player); 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemAction(other.gameObject);

        }
    }
}
