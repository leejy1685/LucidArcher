using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    //�̱���
    public static GameManager Instance;

    //�� �Ŵ���
    [SerializeField] RoomSpawner roomSpawner;
    MonsterSpawner monsterSpawner;

    //UI �Ŵ���
    [SerializeField] UIManager UIManager;

    //�÷��̾�
    [SerializeField] PlayerController player;
    
    //���� ���� ��
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

    //���� ���� �� ������ �ʱ�ȭ
    public void StartGame()
    {
        //�� ����
        roomSpawner.Init();

        //bgm ����
        SoundManager.instance.EndBattle();

        //�÷��� ��
        isPlaying = true;
        Time.timeScale = 1;
    }

    //���� ��� UI ����
    public void GameOver()
    {
        //bgm ����
        SoundManager.instance.EndBattle();

        //�÷��� ����
        isPlaying = false;
        TimeControll();
        UIManager.SetGameOver();
    }

    //���ο� �� �����
    public void CreateRoom(Vector2 position)
    {
        roomSpawner.SpawnRoom(position);
        monsterSpawner = roomSpawner.CurrentRoom.MonsterSpawner;
    }

    //���� ������ �̵�
    public void NextFloor(Vector2 position)
    {
        roomSpawner.MoveNextFloor(position);
    }

    //����ġ ���
    public void AbsorbExp(ExpBall exp)
    {
        //���� ���Ͱ� ������ �̵�
        if (monsterSpawner.MonsterCount == 0)
        {
            exp.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            exp.GetComponent<Rigidbody2D>().gravityScale = 0;
            exp.transform.position = Vector3.Lerp(exp.transform.position, player.transform.position, Time.deltaTime*3);
        }

    }

    //ĳ���� ���ۿ� ����
    private void ManagerHendler()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TimeControll();
        }
    }

    //�ð� ���� ���
    private void TimeControll()
    {
        if(Time.timeScale == 0) 
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

}
