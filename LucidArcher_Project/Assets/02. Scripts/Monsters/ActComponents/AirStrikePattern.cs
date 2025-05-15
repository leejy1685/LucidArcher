using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AirStrikePattern : MonoBehaviour, IEnemyPattern
{
    public MonsterBase Monster { get; set; }
    public GameObject bombPrefab;
    public int bombPoolSize;
    private Queue<PoolElement> bombPool;
    private Location location;
    public float castingTime;
    public int shotCount;

    public void Init(MonsterBase _monster)
    {
        Monster = _monster;
        bombPool = new Queue<PoolElement>();

        for (int i = 0; i < bombPoolSize; i++)
        {
            GameObject shotElGameObject = Instantiate(bombPrefab, Monster.transform.parent);  // 하이어라키 정리가 필요하면 여기서 설정
            PoolElement shotElement = shotElGameObject.GetComponent<PoolElement>();
            shotElement.Init(bombPool);
            shotElGameObject.SetActive(false);
            bombPool.Enqueue(shotElement);
        }
    }

    public void SetLocation(Location _location)
    {
        location = _location;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(Action enterStateAction)
    {
        if (bombPool.Count >= shotCount) StartCoroutine(Strike(enterStateAction));
        else enterStateAction.Invoke();
    }
    
    IEnumerator Strike(Action enterStateAction)
    {
        float shotInterval = castingTime / shotCount;
        
        for(int i = 0; i< shotCount; i++)
        {
            PoolElement shotElement = bombPool.Dequeue();
            shotElement.transform.position = location.GetRandomPointInRect();
            shotElement.gameObject.SetActive(true);

            yield return new WaitForSeconds(shotInterval);
        }
       
        enterStateAction.Invoke();
    }
}
