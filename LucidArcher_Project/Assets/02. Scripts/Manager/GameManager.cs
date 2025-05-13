using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager Instance;

    //룸 매니저
    [SerializeField] RoomSpawner roomSpawner;
    MonsterSpawner monsterSpawner;

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
    }

    void Start()
    {   
        StartGame();
    }

    private void Update()
    {
        ManagerHendler();
    }

    //게임 시작 및 데이터 초기화
    public void StartGame()
    {
        //맵 생성
        roomSpawner.Init();

        //bgm 실행
        SoundManager.instance.EndBattle();

        //플레이 중
        isPlaying = true;
        Time.timeScale = 1;
    }

    //게임 사망 UI 띄우기
    public void GameOver()
    {
        //bgm 실행
        SoundManager.instance.EndBattle();

        //플레이 종료
        isPlaying = false;
        TimeControll();
        UIManager.SetGameOver();
    }

    //새로운 방 만들기
    public void CreateRoom(Vector2 position)
    {
        roomSpawner.SpawnRoom(position);
        monsterSpawner = roomSpawner.CurrentRoom.MonsterSpawner;
    }

    //다음 층으로 이동
    public void NextFloor(Vector2 position)
    {
        roomSpawner.MoveNextFloor(position);
    }

    //경험치 흡수
    public void AbsorbExp(ExpBall exp)
    {
        //방의 몬스터가 없으면 이동
        if (monsterSpawner.MonsterCount == 0)
        {
            exp.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            exp.GetComponent<Rigidbody2D>().gravityScale = 0;
            exp.transform.position = Vector3.Lerp(exp.transform.position, player.transform.position, Time.deltaTime*3);
        }

    }

    //캐릭터 조작외 조작
    private void ManagerHendler()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TimeControll();
        }
    }

    //시간 조작 기능
    private void TimeControll()
    {
        if(Time.timeScale == 0) 
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

}
