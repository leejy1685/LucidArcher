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
    //���� ���� Ű
    [SerializeField] Button[] KeyButtons;

    int index;   //�ٲ� Ű�� �ε���

    //Ȯ�� ��ư
    [SerializeField] Button OkButton;

    public override void InIt(UIManager uIManager)
    {
        base.InIt(uIManager);

        //�ʱ� Ű�� ��������
        for (int i = 0; i < (int)KeyInput.Count; i++)
        {
            KeyButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = KeyManager.keycode[i].ToString();
            KeyButtons[i].onClick.AddListener(ChangeIndex);
        }

        index = 0;

        //��ư�� ��� �ֱ�
        OkButton.onClick.AddListener(OnclickOkButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.KeySettingUI;
    }

    //�ε��� ����
    void ChangeIndex()
    {
        for (int i = 0; KeyButtons.Length > i; i++)
        {   
            //���� Ŭ���� ��ư�� �ε����� ��ȸ
            if(EventSystem.current.currentSelectedGameObject.Equals(KeyButtons[i].gameObject))
            {
                index = i;
                break;
            }
        }
    }



    private void OnGUI()
    {
        //���� Ű�� �������ִ� ����
        Event keyEvent = Event.current;
        if (keyEvent.isKey)
        {
            KeyButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = keyEvent.keyCode.ToString();
            KeyManager.keycode[index] = keyEvent.keyCode;
        }
    }

    void OnclickOkButton()
    {
        uiManager.ChangeState(UIState.Robby);
    }
}
