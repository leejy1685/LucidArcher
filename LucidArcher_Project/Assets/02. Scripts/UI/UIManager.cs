using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Start,
    Robby,
    Game,
    GameOver,
    SoundObtionUI,
    KeySettingUI
}

public class UIManager : MonoBehaviour
{
    // 상수
    private static readonly int IS_BLIND = Animator.StringToHash("IsBlind");
    [SerializeField] private Animator fadeImage;

    private GameUI gameUI;
    private GameOverUI gameOverUI;
    private RobbyUI robbyUI;
    private StartUI startUI;
    private SoundObtionUI soundObtionUI;
    private KeySettingUI keySettingUI;

    private UIState currentState;

    private void Start()
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
        keySettingUI = GetComponentInChildren<KeySettingUI>(true);
        keySettingUI.InIt(this);

        
        ChangeState(UIState.Start);
    }

// 임시 게임 종료 버튼 : Space바
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SetGameOver();
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
        keySettingUI.SetActive(currentState);


    }

    // 페이드 아웃 효과 : 점차 어두워짐
    public void FadeOut()
    {
        fadeImage.SetBool(IS_BLIND, true);
    }

    // 페이드 인 효과 : 점차 밝아짐
    public void FadeIn()
    {
        fadeImage.SetBool(IS_BLIND, false);
    }
}
