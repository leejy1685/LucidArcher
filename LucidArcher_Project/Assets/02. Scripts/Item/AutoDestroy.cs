using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    //아이템 10초뒤에 사라지게 하는 스크립트
    void Start()
    {
        Destroy(gameObject, 10f);
    }


}
