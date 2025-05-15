using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


enum KeyInput
{
    Up,
    Down, 
    Left, 
    Right,
    Dash,
    Count
}
public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    private void Awake()
    {
        instance = this;
    }

    //초기 키 설정
    public KeyCode[] keycode =
    {
        KeyCode.W,
        KeyCode.S,
        KeyCode.A,
        KeyCode.D,
        KeyCode.J
    };

    //좌우 값 리턴
    public float getHorizontal()
    {
        float result = 0;
        if (Input.GetKey(keycode[(int)KeyInput.Left]))
            result -= 1;
        if (Input.GetKey(keycode[(int)KeyInput.Right]))
            result += 1;

        return result;
    }

    //위아래 값 리턴
    public float getVertical()
    {
        float result = 0;
        if (Input.GetKey(keycode[(int)KeyInput.Down]))
            result -= 1;
        if (Input.GetKey(keycode[(int)KeyInput.Up]))
            result += 1;

        return result;
    }


}
