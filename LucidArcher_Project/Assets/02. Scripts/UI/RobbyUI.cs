using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyUI : BaseUI
{
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button startUIButton;

    public override void InIt(UIManager uIManager)
    {
        base.InIt(uIManager);

        gameStartButton.onClick.AddListener(OnClickGameStartButton);
        startUIButton.onClick.AddListener(OnClickStartUIButton);
    }

    public void OnClickGameStartButton()
    {
        uIManager.SetPlayGame();
    }

    public void OnClickStartUIButton()
    {
        uIManager.ChangeState(UIState.Start);
    }

    protected override UIState GetUIState()
    {
        return UIState.Robby;
    }
}
