using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
   public Animator animator;
    public GameObject[] itemPrefabs; //������ ��� ���� ����Ʈ

     
    public float dropForce = 2f; //������ Ƣ������� ��

    


    public void DestroyChest() // ���� ����
    {
        Destroy(gameObject, 1.0f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            animator.SetTrigger("Open"); //Ʈ���� �߻� �� ���ڿ��� �ִϸ��̼� ���


            //DropItems();

        }
  

    }


    void DropItems()
    {

             int itemcount = Random.Range(1,6) ; //��� ������ ����

        for (int i = 0; i < itemcount; i++) // ��� ������ �� ��ŭ �ݺ�
        {
            GameObject item_prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)]; //������ ����߿� �ϳ� ����

            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.15f, 0.15f), -0.2f, 0); //������ ���� ��ġ �������� ����

            GameObject item = Instantiate(item_prefab, spawnPos, Quaternion.identity); //������ ����

            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null) //rigidbody �����
            {
                // ���� + ������ �ణ Ƣ�� ���� ��
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
