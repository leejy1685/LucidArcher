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
    [SerializeField] PlayerController player;
    
    //게임 실행 중
    private bool isPlaying;
    public bool IsPlaying {  get { return isPlaying; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        StartGame();
    }

    void Start()
    {
        roomSpawner.Init();
    }

    //게임 시작 및 데이터 초기화
    public void StartGame()
    {
        isPlaying = true;
        Time.timeScale = 1;
    }

    //게임 사망 UI 띄우기
    public void GameOver()
    {
        isPlaying = false;
        Time.timeScale = 0;
        UIManager.SetGameOver();
    }

    //새로운 방 만들기
    public void CreateRoom(Vector2 position)
    {
        roomSpawner.SpawnRoom(position);
    }

    //다음 층으로 이동
    public void NextFloor(Vector2 position)
    {
        roomSpawner.MoveNextFloor(position);
    }
}
