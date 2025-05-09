using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Game,
    GameOver,
    Robby
}

public class UIManager : MonoBehaviour
{
    private GameUI gameUI;
    private GameOverUI gameOverUI;

    private UIState currentState;

    private void Awake()
    {
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.InIt(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.InIt(this);

        ChangeState(UIState.Game);
    }

// 임시 게임 종료 버튼 : Space바바
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
    }


}
