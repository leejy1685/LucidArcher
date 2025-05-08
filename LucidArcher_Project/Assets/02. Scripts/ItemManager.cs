using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{

    //private string itemName; //아이템 이름인데 쓸 진 아직 모름 아이템 설명 필요할 때 쓸 수도?


    // Update is called once per frame
    public float pickupDelay = 0.5f;
    public float spawnTime;


    private void Start()
    {
        spawnTime = Time.time; 
    }

    public abstract void ItemAction(GameObject player); 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if(Time.time -spawnTime< pickupDelay) {
                return;

        }
            ItemAction(other.gameObject);

        }
    }
}
