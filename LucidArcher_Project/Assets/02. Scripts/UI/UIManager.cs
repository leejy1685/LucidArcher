using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Start,
    Robby,
    Game,
    LevelUp,
    GameOver
}

public class UIManager : MonoBehaviour
{
    private GameUI gameUI;
    private GameOverUI gameOverUI;
    private RobbyUI robbyUI;
    private StartUI startUI;
    private LevelUpUI levelUpUI;

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
        levelUpUI = GetComponentInChildren<LevelUpUI>(true);
        levelUpUI.InIt(this);
        

        ChangeState(UIState.Start);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void PlayerLevelUp()
    {
        ChangeState(UIState.LevelUp);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;

        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
        robbyUI.SetActive(currentState);
        startUI.SetActive(currentState);
        levelUpUI.SetActive(currentState);
    }


}
