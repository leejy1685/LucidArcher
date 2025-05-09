using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    public Slider Exp;
    public TextMeshProUGUI stage;

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    private void Start()
    {
        UpdateExpSlider(0);
    }

    public void UpdateExpSlider(float percentage)
    {
        Exp.value = percentage;
    }

    public void UpdateStageText(int stage)
    {
        this.stage.text = stage.ToString();
    }
}
