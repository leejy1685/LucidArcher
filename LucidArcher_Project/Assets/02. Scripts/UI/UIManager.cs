using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Start,
    Robby,
    Game,
    GameOver,
    SoundObtionUI
}

public class UIManager : MonoBehaviour
{
    private GameUI gameUI;
    private GameOverUI gameOverUI;
    private RobbyUI robbyUI;
    private StartUI startUI;
    private SoundObtionUI soundObtionUI;

    private UIState currentState;

    private void Awake()
    {
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.InIt(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.InIt(this);
        robbyUI = GetComponentInChildren<RobbyUI>(true);
        robbyUI.InIt(this);
        startUI = GetComponentInChildren<StartUI>(true);
        startUI.InIt(this);
        soundObtionUI = GetComponentInChildren<SoundObtionUI>(true);
        soundObtionUI.InIt(this);

        ChangeState(UIState.Start);
    }

// 임시 게임 종료 버튼 : Space바
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetGameOver();
        }
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;

        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
        robbyUI.SetActive(currentState);
        startUI.SetActive(currentState);
        soundObtionUI.SetActive(currentState);


    }


}
