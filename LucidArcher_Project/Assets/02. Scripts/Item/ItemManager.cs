using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{



    //private string itemName; //������ �̸��ε� �� �� ���� �� ������ ���� �ʿ��� �� �� ����?

    public float itemY; //����� �������� ����� �� ��ǥ
    public float tolerance = 0.05f; //��������
    private Rigidbody2D rb;

    public float pickupDelay = 0.5f; // ������ �ٷ� �Ծ����� �ȵǴ� ������
    public float spawnTime; // ������ ������ �ð� ����

    private float gravityDelay = 0.3f; // ������ �������ڸ��� �߷� 0�Ǵ� �� ���� ������

    public GameObject Monster; //���� ���� ������Ʈ
    private void Start()
    {
        spawnTime = Time.time;  //������ ������ �ð� ���

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Time.time - spawnTime < gravityDelay) 
        {

            return;
        }

        if((transform.position.y - itemY) < tolerance) //�������� �������
        {

            rb.gravityScale = 0; 
            rb.velocity = Vector2.zero; //����
            this.enabled = false;

        }

        
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
