using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : BaseUI
{
    [SerializeField] private Button robbyButton;
    [SerializeField] private Button exitButton;

    public override void InIt(UIManager uIManager)
    {
        base.InIt(uIManager);

        robbyButton.onClick.AddListener(OnClickRobbyButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRobbyButton()
    {
        uIManager.ChangeState(UIState.Robby);
    }

    public void OnClickExitButton()
    {
        // 게임 종료 OR StartUI로 돌아가기기
    }

    protected override UIState GetUIState()
    {
        return UIState.Start;
    }
}
