using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolElement : MonoBehaviour
{

    Queue<PoolElement> home;
    public void InIt (Queue<PoolElement> _home)
    {
        home = _home;       
    }

    // Update is called once per frame
    public void Retrive()
    {
        if (home == null) Destroy(gameObject);
        else
        {
            home.Enqueue(this);
            gameObject.SetActive(false);
        }
    }
}
