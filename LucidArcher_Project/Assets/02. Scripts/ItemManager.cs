using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{

    //private string itemName; //아이템 이름인데 쓸 진 아직 모름 아이템 설명 필요할 때 쓸 수도?


    // Update is called once per frame


    //아이템 효과 발동
    public abstract void ItemAction(GameObject player); 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemAction(other.gameObject);

        }
    }
}
