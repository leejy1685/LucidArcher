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
    public RoomSpawner RoomSpawner;
    public MonsterSpawner monsterSpawner;

    //UI 매니저
    [SerializeField] UIManager UIManager;

    public LevelUpUI levelUpUI;
    public WeaponStat weaponStat;

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

    private void Start()
    {
        weaponStat = player.GetComponentInChildren<WeaponStat>(true);
    }

    private void Update()
    {
        ManagerHendler();
    }

    void FixedUpdate()
    {
        if (player.Stat.MaxEXP <= player.Stat.EXP)
        {
            UIManager.PlayerLevelUp();
        }
        player.Stat.LevelUP();
    }

    public void AttackDamageUp()
    {
        weaponStat.UpgradeDamage();
        UIManager.ChangeState(UIState.Game);
    }

    public void AttackDelayUp()
    {
        player.Stat.UpgradeAttackDelay();
        UIManager.ChangeState(UIState.Game);
    }

    public void MaxHpUp()
    {
        player.Stat.UpgradeMaxHP();
        UIManager.ChangeState(UIState.Game);
    }

    public void PlayerSpeedUp()
    {
        player.Stat.UpgradePlayerSpeed();
        UIManager.ChangeState(UIState.Game);
    }

    public void BulletNumUp()
    {
        weaponStat.UpgradeBulletNum();
        UIManager.ChangeState(UIState.Game);
    }

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }

    public PlayerStatHendler GetPlayerStatInfo()
    {
        return player.Stat;
    }

    //게임 시작 및 데이터 초기화
    public void StartGame()
    {
        //맵 생성
        RoomSpawner.Init();

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
        Time.timeScale = 0;
        UIManager.SetGameOver();
    }

    //새로운 방 만들기
    public void CreateRoom(Vector2 position)
    {
        RoomSpawner.SpawnRoom(position);
        monsterSpawner = RoomSpawner.CurrentRoom.MonsterSpawner;
    }

    //경험치 흡수
    public void AbsorbExp(ExpBall exp)
    {

        //방의 몬스터가 없으면 이동
        if (monsterSpawner.MonsterCount <= 0)
        {
            exp.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            exp.GetComponent<Rigidbody2D>().gravityScale = 0;
            exp.transform.position = Vector3.Lerp(exp.transform.position, player.transform.position, Time.deltaTime * 3);
        }

    }

    //캐릭터 조작외 조작
    private void ManagerHendler()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            RobbyUIKey();
        }
    }

    public void RobbyUIKey()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            UIManager.ChangeState(UIState.Game);
        }
        else
        {
            Time.timeScale = 0;
            UIManager.ChangeState(UIState.Robby);
        }
    }

    // 애니메이션 진행 중 플레이어 움직임 제한
    public void PauseGame(bool isAnimationPlaying)
    {
        isPlaying = !isAnimationPlaying;
    }
}
