using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{



    //private string itemName; //아이템 이름인데 쓸 진 아직 모름 아이템 설명 필요할 때 쓸 수도?

    public float itemY; //드랍될 아이템이 멈춰야 할 좌표
    public float tolerance = 0.05f; //오차범위
    private Rigidbody2D rb;

    public float pickupDelay = 0.5f; // 아이템 바로 먹어지면 안되니 딜레이
    public float spawnTime; // 아이템 생성된 시간 변수

    private float gravityDelay = 0.3f; // 아이템 스폰되자마자 중력 0되는 거 방지 딜레이

    public GameObject Monster; //몬스터 게임 오브젝트
    private void Start()
    {
        spawnTime = Time.time;  //아이템 생성시 시간 기록

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Time.time - spawnTime < gravityDelay) 
        {

            return;
        }

        if((transform.position.y - itemY) < tolerance) //오차보다 낮을경우
        {

            rb.gravityScale = 0; 
            rb.velocity = Vector2.zero; //멈춤
            this.enabled = false;

        }

        
    }

    public abstract void ItemAction(GameObject player);  //자식 클래스에서 구현 될 메서드


    private void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.CompareTag("Player"))
        {

            if (Time.time - spawnTime < pickupDelay)  // 스폰되고 부딪힌 시간이 pickupDelay보다 빠르면 안먹어짐
            { 
                return;

            }
            ItemAction(other.gameObject); //아이템 고유 효과 발동
            
        }
    }
}
