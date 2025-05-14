using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyUI : BaseUI
{
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button startUIButton;

    //¼³Á¤ UI µé
    [SerializeField] private Button KeySettingUIButton;
    [SerializeField] private Button SoundSettingUIButton;

    public override void InIt(UIManager uIManager)
    {
        base.InIt(uIManager);

        gameStartButton.onClick.AddListener(OnClickGameStartButton);
        startUIButton.onClick.AddListener(OnClickStartUIButton);

        KeySettingUIButton.onClick.AddListener(OnClickKeySetting);
        SoundSettingUIButton.onClick.AddListener(OnCkickSoundSetting);
    }

    public void OnClickGameStartButton()
    {
        GameManager.Instance.RobbyUIKey();
    }

    void OnClickKeySetting()
    {
        uiManager.ChangeState(UIState.KeySettingUI);
    }

    void OnCkickSoundSetting()
    {
        uiManager.ChangeState(UIState.SoundObtionUI);
    }

    public void OnClickStartUIButton()
    {
        uiManager.ChangeState(UIState.Start);
    }

    protected override UIState GetUIState()
    {
        return UIState.Robby;
    }
}
