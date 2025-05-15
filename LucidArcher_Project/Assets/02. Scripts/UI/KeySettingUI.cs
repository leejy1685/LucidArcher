using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeySettingUI : BaseUI
{
    //변경 가능 키
    [SerializeField] Button[] KeyButtons;

    int index;   //바꿀 키의 인덱스

    //확인 버튼
    [SerializeField] Button OkButton;

    public override void InIt(UIManager uIManager)
    {
        base.InIt(uIManager);

        //초기 키값 가져오기
        for (int i = 0; i < (int)KeyInput.Count; i++)
        {
            KeyButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = KeyManager.instance.keycode[i].ToString();
            KeyButtons[i].onClick.AddListener(ChangeIndex);
        }

        index = 0;

        //버튼에 기능 넣기
        OkButton.onClick.AddListener(OnclickOkButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.KeySettingUI;
    }

    //인덱스 변경
    void ChangeIndex()
    {
        for (int i = 0; KeyButtons.Length > i; i++)
        {   
            //현재 클릭한 버튼의 인덱스를 조회
            if(EventSystem.current.currentSelectedGameObject.Equals(KeyButtons[i].gameObject))
            {
                index = i;
                break;
            }
        }
    }



    private void OnGUI()
    {
        //실제 키를 변경해주는 역할
        Event keyEvent = Event.current;
        if (keyEvent.isKey)
        {
            KeyButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = keyEvent.keyCode.ToString();
            KeyManager.instance.keycode[index] = keyEvent.keyCode;
        }
    }

    void OnclickOkButton()
    {
        uiManager.ChangeState(UIState.Robby);
    }
}
