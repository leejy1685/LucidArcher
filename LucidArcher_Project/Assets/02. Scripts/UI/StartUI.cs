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
        GameManager.Instance.StartGame();
        uiManager.ChangeState(UIState.Robby);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    protected override UIState GetUIState()
    {
        return UIState.Start;
    }
}
