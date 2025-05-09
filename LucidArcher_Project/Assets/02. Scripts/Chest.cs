using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
   public Animator animator;
    public GameObject[] itemPrefabs; //아이템 목록 담을 리스트

     
    public float dropForce = 2f; //아이템 튀어오를때 힘

    


    public void DestroyChest() // 상자 제거
    {
        Destroy(gameObject, 1.0f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            animator.SetTrigger("Open"); //트리거 발생 시 상자여는 애니메이션 재생


            //DropItems();

        }
  

    }


    void DropItems()
    {

             int itemcount = Random.Range(1,6) ; //드랍 아이템 개수

        for (int i = 0; i < itemcount; i++) // 드랍 아이템 수 만큼 반복
        {
            GameObject item_prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)]; //아이템 목록중에 하나 정함

            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.15f, 0.15f), -0.2f, 0); //아이템 나올 위치 랜덤으로 설정

            GameObject item = Instantiate(item_prefab, spawnPos, Quaternion.identity); //아이템 생성

            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null) //rigidbody 존재시
            {
                // 위로 + 옆으로 약간 튀는 랜덤 힘
                Vector2 force = new Vector2(Random.Range(-1f, 1f), 1f) * dropForce;
                rb.AddForce(force, ForceMode2D.Impulse);
            }

            ItemManager drop = item.GetComponent<ItemManager>();
            if(drop != null)
            {

                drop.itemY = transform.position.y;
            }

        }


    }
}
