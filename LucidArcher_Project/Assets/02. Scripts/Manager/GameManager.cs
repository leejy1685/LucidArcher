using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager Instance;

    //룸 매니저
    [SerializeField] RoomSpawner roomSpawner;

    //UI 매니저
    [SerializeField] UIManager UIManager;

    //플레이어
    public GameObject player;
    
    //게임 실행 중
    private bool isPlaying;
    public bool IsPlaying {  get { return isPlaying; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        isPlaying = true;
    }

    void Start()
    {
        roomSpawner.Init();
    }

    public void GameOver()
    {
        isPlaying = false;
        UIManager.SetGameOver();
    } 

}
