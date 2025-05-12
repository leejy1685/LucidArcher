using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] RoomSpawner roomSpawner;
    [SerializeField] UIManager UIManager;
    public GameObject player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        roomSpawner.Init();
    }

}
