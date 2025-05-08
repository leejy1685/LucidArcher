using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
   public Animator animator;
    public GameObject[] itemPrefabs;

    public int itemcount = 5 ;
    public float dropForce = 2f;
    
    

    public void DestroyChest()
    {
        Destroy(gameObject, 1.0f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            animator.SetTrigger("Open");


            //DropItems();

        }
  

    }


    void DropItems()
    {

        for (int i = 0; i < itemcount; i++)
        {
            GameObject item_prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.2f, 0.2f), -0.2f, 0);

            GameObject item = Instantiate(item_prefab, spawnPos, Quaternion.identity);

            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // À§·Î + ¿·À¸·Î ¾à°£ Æ¢´Â ·£´ý Èû
                Vector2 force = new Vector2(Random.Range(-1f, 1f), 1f) * dropForce;
                rb.AddForce(force, ForceMode2D.Impulse);
            }

        }


    }
}
