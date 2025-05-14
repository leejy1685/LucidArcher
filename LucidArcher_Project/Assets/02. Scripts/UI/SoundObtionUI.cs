using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundObtionUI : BaseUI
{
    [SerializeField]Slider BGM;
    [SerializeField] Slider SFX;
    [SerializeField] Button OkButton;

    public override void InIt(UIManager uiManager)
    {
        base.InIt(uiManager);

        BGM.value = SoundManager.instance.MusicVolume;
        SFX.value = SoundManager.instance.SoundEffectVolume;

        OkButton.onClick.AddListener(OnclickOkButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.SoundObtionUI;
    }

    void OnclickOkButton()
    {
        SoundManager.instance.MusicVolume = BGM.value;
        SoundManager.instance.SoundEffectVolume = SFX.value;

        GameManager.Instance.SoundOptionUIKey();
    }
}
