using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyUI : BaseUI
{
    [SerializeField] private Button GameStartButton;
    [SerializeField] private Button ExitButton;

    

    protected override UIState GetUIState()
    {
        return UIState.Robby;
    }
}
