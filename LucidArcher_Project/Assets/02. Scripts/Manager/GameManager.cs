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
    public GameObject player;
    
    //���� ���� ��
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
