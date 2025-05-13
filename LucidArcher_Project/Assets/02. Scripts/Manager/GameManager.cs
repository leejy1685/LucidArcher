using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(monsterSpawner.MonsterCount == 0)
        {
            //���ư����ϴ� ���� ���
            Vector3 expPosition = exp.transform.position;
            Vector2 direction = (player.transform.position - expPosition).normalized * 10f;

            //�ӵ��� �ο�
            exp.GetComponent<Rigidbody2D>().velocity = direction;

        }
    }

}
