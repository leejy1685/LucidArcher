using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button robbyButton;

    public override void InIt(UIManager uIManager)
    {
        base.InIt(uIManager);
        retryButton.onClick.AddListener(OnclickRetryButton);
        robbyButton.onClick.AddListener(OnClickRobbyButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    public void OnclickRetryButton()
    {
        //uIManager.ChangeState(UIState.Game);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickRobbyButton()
    {
        uIManager.ChangeState(UIState.Robby);
    }
}
