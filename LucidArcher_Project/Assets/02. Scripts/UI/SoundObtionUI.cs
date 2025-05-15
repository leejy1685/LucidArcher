using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundObtionUI : BaseUI
{
    [SerializeField] Slider BGM;
    [SerializeField] Slider SFX;
    [SerializeField] Button OkButton;

    public override void InIt(UIManager uiManager)
    {
        base.InIt(uiManager);

        BGM.value = SoundManager.MusicVolume;
        SFX.value = SoundManager.SoundEffectVolume;

        OkButton.onClick.AddListener(OnclickOkButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.SoundObtionUI;
    }

    void OnclickOkButton()
    {
        SoundManager.MusicVolume = BGM.value;
        SoundManager.SoundEffectVolume = SFX.value;

        uiManager.ChangeState(UIState.Robby);
    }
}
