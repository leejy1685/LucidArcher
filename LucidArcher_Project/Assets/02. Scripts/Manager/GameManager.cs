using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�̱���
    public static GameManager Instance;

    //�� �Ŵ���
    [SerializeField] RoomSpawner roomSpawner;

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

        StartGame();
    }

    void Start()
    {
        roomSpawner.Init();
    }

    //���� ���� �� ������ �ʱ�ȭ
    public void StartGame()
    {
        isPlaying = true;
        Time.timeScale = 1;
    }

    //���� ��� UI ����
    public void GameOver()
    {
        isPlaying = false;
        Time.timeScale = 0;
        UIManager.SetGameOver();
    }

    //���ο� �� �����
    public void CreateRoom(Vector2 position)
    {
        roomSpawner.SpawnRoom(position);
    }

    //���� ������ �̵�
    public void NextFloor(Vector2 position)
    {
        roomSpawner.MoveNextFloor(position);
    }
}
